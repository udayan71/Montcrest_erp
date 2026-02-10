using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Configuration;
using Montcrest.BLL.Interfaces;

namespace Montcrest.BLL.Services
{
    public class GoogleSheetService : IGoogleSheetService
    {
        private readonly IConfiguration _config;

        public GoogleSheetService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int?> GetCandidateScoreAsync(string candidateEmail)
        {
            var credentialsPath = _config["GoogleSheets:CredentialsPath"];
            var spreadsheetId = _config["GoogleSheets:SpreadsheetId"];
            var sheetName = _config["GoogleSheets:SheetName"];

            if (string.IsNullOrWhiteSpace(credentialsPath) ||
                string.IsNullOrWhiteSpace(spreadsheetId) ||
                string.IsNullOrWhiteSpace(sheetName))
            {
                throw new Exception("GoogleSheets settings missing in appsettings.json");
            }

            if (!File.Exists(credentialsPath))
            {
                throw new Exception($"Google credentials file not found: {credentialsPath}");
            }

            GoogleCredential credential;

            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MontcrestERP"
            });

            // Read entire sheet
            var range = $"{sheetName}!A:Z";
            var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = await request.ExecuteAsync();

            var values = response.Values;

            if (values == null || values.Count == 0)
                return null;

            // First row = headers
            var headers = values[0].Select(h => h.ToString()?.Trim()).ToList();

            int emailIndex = headers.FindIndex(h =>
                !string.IsNullOrWhiteSpace(h) &&
                h.Equals("Email Address", StringComparison.OrdinalIgnoreCase));

            int scoreIndex = headers.FindIndex(h =>
                !string.IsNullOrWhiteSpace(h) &&
                h.Equals("Score", StringComparison.OrdinalIgnoreCase));

            if (emailIndex == -1)
                throw new Exception("Google Sheet does not contain column named 'Email Address'");

            if (scoreIndex == -1)
                throw new Exception("Google Sheet does not contain column named 'Score'");

            // Search row by email
            foreach (var row in values.Skip(1))
            {
                if (row.Count <= emailIndex)
                    continue;

                var email = row[emailIndex]?.ToString()?.Trim();

                if (!string.IsNullOrWhiteSpace(email) &&
                    email.Equals(candidateEmail, StringComparison.OrdinalIgnoreCase))
                {
                    if (row.Count <= scoreIndex)
                        return null;

                    var scoreText = row[scoreIndex]?.ToString()?.Trim();

                    if (string.IsNullOrWhiteSpace(scoreText))
                        return null;

                    // Handles: "100 / 100"
                    if (scoreText.Contains("/"))
                        scoreText = scoreText.Split('/')[0].Trim();

                    if (int.TryParse(scoreText, out int score))
                        return score;

                    return null;
                }
            }

            return null;
        }
    }
}
