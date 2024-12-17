namespace company.ass.pl.helpers
{
    public static class DocumentSettings
    {   //uplaod
        public static string UplaodFile(IFormFile file, string foldername)
        {
            //1-get location folder path 
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(),$"wwwroot\\files\\{foldername}");

            //2-get file name and doing this file riquerd
            string filename = $"{Guid.NewGuid()}{file.FileName}";

            //3- get file path => filename + folderpath

            string filepath = Path.Combine(folderpath, filename);

            //4- save file as stream
          using var filestream = new FileStream(filepath, FileMode.Create);


            file.CopyTo(filestream);
            return filename;
        }

        //delete
        public static void DeleteFile(string filename , string foldername)
        { 
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/files", foldername ,filename);

            if(File.Exists(filepath))
                File.Delete(filepath);
        }
    }
}
