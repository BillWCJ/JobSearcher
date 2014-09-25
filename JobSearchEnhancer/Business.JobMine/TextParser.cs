using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobs;
using GlobalVariable;

namespace JobMine
{
    public class TextParser
    {
        public static Job ExtractTextFileJobInfo(string sourceString, string url) //todo: improve
        {
            string[] fields = new string[GVar.JobDetailPageFieldNameTitles.Length];
            int indexStart = 0;
            int indexEnd = 0;
            for (int i = 0; i < GVar.JobDetailPageFieldNameTitles.Length - 1; i++)
            {
                indexStart = sourceString.IndexOf(GVar.JobDetailPageFieldNameTitles[i], indexStart) + GVar.JobDetailPageFieldNameTitles[i].Length;
                indexEnd = sourceString.IndexOf(GVar.JobDetailPageFieldNameTitles[i + 1], indexStart);
                fields[i] = sourceString.Substring(indexStart, indexEnd - indexStart).TrimEnd('\n');
            }
            fields[7] = url;

            return new Job(fields);
        }
        public static Job[] ReadInJobFromLocal()
        {
            int indexStart = 0;
            int indexEnd = 0;
            Queue<string> jobID = GetJobIDFromLocal();
            int numberOfJob = jobID.Count;
            Job[] jobs = new Job[numberOfJob];
            string data = String.Empty;
            StreamReader reader = StreamReader.Null;

            try
            {
                reader = new StreamReader(GVar.FilePath + "Jobs.txt");
                data = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_ReadInJobFromLocal: {1}\n", e.ToString(), e);
            }
            try
            {
                for (int i = 0; i < numberOfJob; i++)
                {
                    indexStart = data.IndexOf(GVar.SeperationBar, indexStart) + GVar.SeperationBar.Length;
                    if (i != numberOfJob - 1)
                    {
                        indexEnd = data.IndexOf(GVar.SeperationBar, indexStart);
                    }
                    else
                    {
                        indexEnd = data.Length;
                    }
                    jobs[i] = ExtractTextFileJobInfo(data.Substring(indexStart, indexEnd - indexStart), GVar.JobDetailBaseUrl + jobID.Dequeue());
                }
            }
            finally
            {
                reader.Close();
            }

            return jobs;
        }

        public static Queue<string> GetJobIDFromLocal()
        {
            Queue<string> jobID = new Queue<string>();
            StreamReader reader = OpenFileForStreamReader(GVar.FilePath, "JobList.txt");
            if (reader != null)
            {
                while (!reader.EndOfStream)
                {
                    string jobIdString = reader.ReadLine();
                    if (JobMine.JobInquiry.IsCorrectJobID(jobIdString))
                        jobID.Enqueue(jobIdString);
                }
                reader.Close();
            }
            return jobID;
        }
        public static StreamWriter OpenFileForStreamWriter(string fileLocation, string fileName)
        {
            StreamWriter writer = StreamWriter.Null;
            if (fileName.IndexOf(".txt") == -1)
            {
                fileName += ".txt";
            }
            try
            {
                writer = new StreamWriter(fileLocation + fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}-{1}:{2}-{3}:{4}", System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString(),
                    e.Message, e.StackTrace, e.Data);
            }
            return writer;
        }
        public static StreamReader OpenFileForStreamReader(string fileLocation, string fileName)
        {
            StreamReader reader = StreamReader.Null;
            if (fileName.IndexOf(".txt") == -1)
            {
                fileName += ".txt";
            }
            try
            {
                reader = new StreamReader(fileLocation + fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}-{1}:{2}-{3}:{4}", System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString(),
                    e.Message, e.StackTrace, e.Data);
            }
            return reader;
        }

    }
}
