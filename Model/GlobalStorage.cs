
using SB_MAUI.ViewModel;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace SB_MAUI.Model
{
    public static class GlobalStorage
    {
        // Local filename to store the content entries
        static string StorageFilename = "SuperbowlFY23.txt";
 
        // Local filename to store the AWS Credentials
        static string CredsFilename = "AWSFY23.txt";

        // Live storage of the contest entries
        static ObservableCollection<ContestEntry> entries = new ObservableCollection<ContestEntry>();

        // Live storage of the AWS Credentials
        static AWS_Creds AWSCreds = new AWS_Creds();

        public static ObservableCollection<ContestEntry> GetContestEntries()
        {
            return entries;
        }
        public static AWS_Creds GetCcredentials()
        {
            return AWSCreds;
        }


        /// <summary>
        /// Save the live versions of the storage to storage
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SaveStateAsync()
        {
            try
            {
                string JSONText = "{}";

                string FullPath = FileSystem.AppDataDirectory + Path.PathSeparator + StorageFilename;
                JSONText = JsonSerializer.Serialize((ObservableCollection<ContestEntry>)entries);
                await File.WriteAllTextAsync(FullPath, JSONText);

                FullPath = FileSystem.AppDataDirectory + Path.PathSeparator + CredsFilename;
                JSONText = JsonSerializer.Serialize((AWS_Creds)AWSCreds);
                await File.WriteAllTextAsync(FullPath, JSONText);

            }
            catch
            {
                return false;
            }

            return true;


        }

        /// <summary>
        /// Load the contest entries and AWS Credentials from storage
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> LoadStateAsync()
        {

            try
            {
                string JSONText = "{}";

                string FullPath = FileSystem.AppDataDirectory + Path.PathSeparator + StorageFilename;
                JSONText = await File.ReadAllTextAsync(FullPath);

                ObservableCollection<ContestEntry> temp = JsonSerializer.Deserialize<ObservableCollection<ContestEntry>>(JSONText);

                entries.Clear();

                foreach (var ent in temp)
                {
                    entries.Add(ent);
                }

                /////////////////////////////////////
                FullPath = FileSystem.AppDataDirectory + Path.PathSeparator + CredsFilename;
                JSONText = await File.ReadAllTextAsync(FullPath);

                AWS_Creds tempCreds = JsonSerializer.Deserialize<AWS_Creds>(JSONText);

                AWSCreds.AccessKey = tempCreds.AccessKey;
                AWSCreds.SecretKey = tempCreds.SecretKey;
                AWSCreds.BucketName = tempCreds.BucketName;
                AWSCreds.Filename = tempCreds.Filename; 



            }
            catch
            {
                return false;
            }

            return true;




        }

        /// <summary>
        /// Delete the Live contest entries and the contest entries in storage
        /// Leave the AWS Credentials alone
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> ResetStateAsync()
        {

            string FullPath = FileSystem.AppDataDirectory + Path.PathSeparator + StorageFilename;

            try
            {
                entries.Clear();

                File.Delete(FullPath);

                await Task.Delay(10);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
