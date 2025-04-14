namespace Doctor_sAppointment.Helpers
{
    public static class DocumentaSettings 
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            if (file == null || file.Length == 0)
                return null;

            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            string FilePath = Path.Combine(FolderPath, FileName);

            using var Fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(Fs);

            return FileName;
        }
        public static void DeleteFile(string FileName, string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
