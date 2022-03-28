﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace DuplicateFilesInFolders
{
    public partial class Form1 : Form
    {
        private const int LOADING = -1;
        private const int CHECK_DUPLICATES = -2;
        private string[] directories;
        private string result;
        private string currentFolder;
        private int numberOfFiles;
        private int processedFiles;
        private readonly string separation = "---------------------";

        public Form1()
        {
            InitializeComponent();

            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            searchButton.Enabled = false;
            cancelAsyncButton.Enabled = true;
            currentFolder = "";
            numberOfFiles = 0;
            processedFiles = 0;
            directories = richTextBoxFolderPaths.Lines;
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
            Console.ReadLine();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // Perform a time consuming operation and report progress.

            worker.ReportProgress(LOADING);
            result = "";
            List<FilePOCO> allFiles = new List<FilePOCO>();
            List<string> allDirs = new List<string>();

            //each folder
            foreach (string baseFolder in directories)
            {
                if (!string.IsNullOrWhiteSpace(baseFolder))
                {
                    // Get all sub dirs
                    allDirs.AddRange(getDirs(baseFolder));
                }
            }

            numberOfFiles = getNumberOfFiles(allDirs);

            foreach (string folder in allDirs)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                currentFolder = getLastElementOfPath(folder);
                result += folder + Environment.NewLine + Environment.NewLine;

                string[] files = Directory.GetFiles(folder);
                int i = 0;
                foreach (string file in files)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    FilePOCO fichier = new FilePOCO(getLastElementOfPath(file), folder, calculateMD5(file));
                    allFiles.Add(fichier);
                    processedFiles = allFiles.Count();
                    result += fichier.name + Environment.NewLine;

                    i++;
                    worker.ReportProgress((i * 100) / files.Count());
                }
                result += Environment.NewLine + Environment.NewLine;
            }

            result += separation + Environment.NewLine + "Duplicates" + Environment.NewLine + separation + Environment.NewLine + Environment.NewLine;

            worker.ReportProgress(CHECK_DUPLICATES);

            var duplicates = from c in allFiles
                                group c by c.hash into g
                                where g.Skip(1).Any()
                                from c in g
                                select c;

            int index = 0;
            string lastHash = "";
            foreach (FilePOCO dupe in duplicates)
            {
                if (lastHash.Equals(dupe.hash))
                {
                    result += "del \"" + dupe.directory + Path.DirectorySeparatorChar + dupe.name + "\"" + Environment.NewLine;
                } else
                {
                    result += "REM Duplication of \"" + dupe.directory + Path.DirectorySeparatorChar + dupe.name + "\"" + Environment.NewLine;
                } 
                index++;
                lastHash = dupe.hash;
            }

            string output = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "DuplicateFilesInFolders-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt";

            File.WriteAllText(output, result);
        }

        // This event handler updates the progress.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage >= 0)
            {
                resultLabel.Text = ("Files processed: " + processedFiles + "/" + numberOfFiles + " (" + (processedFiles * 100) / numberOfFiles + "%)" +
                    "\r\n" + "Current folder: " + currentFolder + " (" + e.ProgressPercentage.ToString() + "%)");
            } else if (e.ProgressPercentage == LOADING)
            {
                resultLabel.Text = "Loading...";
            }
            else if (e.ProgressPercentage == CHECK_DUPLICATES)
            {
                resultLabel.Text = "Check duplicates...";
            }

        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
            searchButton.Enabled = true;
            cancelAsyncButton.Enabled = false;
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
            cancelAsyncButton.Enabled = false;
            searchButton.Enabled = true;
        }

        private List<string> getDirs(string dirPath)
        {
            List<string> dirs = new List<string>();
            try
            {
                dirs = new List<string>(Directory.EnumerateDirectories(dirPath, "*.*", SearchOption.AllDirectories));
                dirs.Add(dirPath);
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
            return dirs;
        }

        private string calculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
        }

        private string getLastElementOfPath(string path)
        {
            return path.Substring(path.LastIndexOf("\\") + 1);
        }

        private int getNumberOfFiles(List<string> allDirs)
        {
            int total = 0;

            foreach (string folder in allDirs)
            {
                total+= Directory.GetFiles(folder).Length;
            }

            return total;
        }
    }
}
