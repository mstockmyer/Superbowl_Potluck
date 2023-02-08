using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using SB_MAUI.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace SB_MAUI.ViewModel
{
    public class ContestViewModel : ObservableViewModelBase
    {

        /// <summary>
        /// Local entries to expose to the XAML
        /// </summary>
        public ObservableCollection<ContestEntry> Entries { get; set; }

        /// <summary>
        /// Currently selected ContestEntry - so the Edit page works
        /// </summary>
        private static ContestEntry _CurrentContestEntry;
        public ContestEntry CurrentContestEntry
        {
            get => _CurrentContestEntry;
            set => SetProperty(ref _CurrentContestEntry, value);
        }



        public ContestViewModel() 
        {
            // Load the contest entries from GlobalStorage
            Entries = GlobalStorage.GetContestEntries();    

        }

        /// <summary>
        /// Save the current live data to storage
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAsync()
        {
            return await GlobalStorage.SaveStateAsync();

        }

        /// <summary>
        /// Load data from storage into the live data
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadAsync()
        {
            return await GlobalStorage.LoadStateAsync();

        }

        /// <summary>
        /// Upload the contest entries into AWS
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UploadAsync()
        {
            try
            {
                AWS_Creds LocalCreds = GlobalStorage.GetCcredentials();

                BasicAWSCredentials creds = new BasicAWSCredentials(LocalCreds.AccessKey, LocalCreds.SecretKey);

                var client3 = new AmazonS3Client(creds, Amazon.RegionEndpoint.USEast2);

                // Put the JSON version of the contest entries into an S3 bucket, making sure to mark it 
                // as publically accessible
                var request3 = new PutObjectRequest()
                {
                    BucketName = LocalCreds.BucketName,
                    ContentBody = JsonSerializer.Serialize((ObservableCollection<ContestEntry>)Entries),
                    Key = LocalCreds.Filename,
                    CannedACL = S3CannedACL.PublicRead
                };

                var resp3 = await client3.PutObjectAsync(request3);

            }
            catch
            {
                return false;
            }

            return true;
        }

                
        /// <summary>
        /// Clear and delete contest entries
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteAllAsync()
        {
            CurrentContestEntry = null;

            return (await GlobalStorage.ResetStateAsync());

        }

        /// <summary>
        /// Add a new blank contest entry
        /// </summary>
        public void NewEntry()
        {
            ContestEntry entry = new ContestEntry();
            entry.Name = "** NEW **";
            entry.A_to_B = 0;
            entry.B_to_C = 0;
            entry.C_to_A = 0;

            Entries.Add(entry);
        }


        /// <summary>
        /// TESTING ONLY: Create a set of simulated contest entries
        /// </summary>
        public void SimulateLeaderboard()
        {
            ContestEntry entry;

            Entries.Clear();

            entry = new ContestEntry();
            entry.Name = "D5521 Team #1";
            entry.A_to_B = 5;
            entry.B_to_C = 3;
            entry.C_to_A = 4;
            Entries.Add(entry);

            entry = new ContestEntry();
            entry.Name = "D5521 Team #2";
            entry.A_to_B = 7;
            entry.B_to_C = 6;
            entry.C_to_A = 3;
            Entries.Add(entry);

            entry = new ContestEntry();
            entry.Name = "D5522 Team #1";
            entry.A_to_B = 9;
            entry.B_to_C = 2;
            entry.C_to_A = 7;
            Entries.Add(entry);

            entry = new ContestEntry();
            entry.Name = "D5522 Team #2";
            entry.A_to_B = 8;
            entry.B_to_C = 9;
            entry.C_to_A = 2;
            Entries.Add(entry);

            entry = new ContestEntry();
            entry.Name = "D5522 Team #3";
            entry.A_to_B = 6;
            entry.B_to_C = 7;
            entry.C_to_A = 11;
            Entries.Add(entry);
        }
    }
}
