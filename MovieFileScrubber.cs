using System;
using System.IO;
using System.Linq;
using NLog;
using NLog.Web;

namespace MediaLibrary
{
    public static class MovieFileScrubber
    {
        private static readonly Logger logger = NLogBuilder
            .ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public static string ScrubMovies(string readFile)
        {
            try
            {
                // determine name of writeFile
                var ext = readFile.Split('.').Last();
                var writeFile = readFile.Replace(ext, $"scrubbed.{ext}");
                // if writeFile exists, the file has already been scrubbed
                if (File.Exists(writeFile))
                {
                    // file has already been scrubbed
                    logger.Info("File already scrubbed");
                }
                else
                {
                    // file has not been scrubbed
                    logger.Info("File scrub started");
                    // open write file
                    var sw = new StreamWriter(writeFile);
                    // open read file
                    var sr = new StreamReader(readFile);
                    // remove first line - column headers
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        // create instance of Movie class
                        var movie = new Movie();
                        var line = sr.ReadLine();
                        // look for quote(") in string
                        // this indicates a comma(,) or quote(") in movie title
                        var idx = line.IndexOf('"');
                        var genres = "";
                        if (idx == -1)
                        {
                            // no quote = no comma or quote in movie title
                            // movie details are separated with comma(,)
                            var movieDetails = line.Split(',');
                            movie.mediaId = ulong.Parse(movieDetails[0]);
                            movie.title = movieDetails[1];
                            genres = movieDetails[2];
                            movie.director = movieDetails.Length > 3 ? movieDetails[3] : "unassigned";
                            movie.runningTime = movieDetails.Length > 4
                                ? TimeSpan.Parse(movieDetails[4])
                                : new TimeSpan(0);
                        }
                        else
                        {
                            // quote = comma or quotes in movie title
                            // extract the movieId
                            movie.mediaId = ulong.Parse(line.Substring(0, idx - 1));
                            // remove movieId and first comma from string
                            line = line.Substring(idx);
                            // find the last quote
                            idx = line.LastIndexOf('"');
                            // extract title
                            movie.title = line.Substring(0, idx + 1);
                            // remove title and next comma from the string
                            line = line.Substring(idx + 2);
                            // split the remaining string based on commas
                            var details = line.Split(',');
                            // the first item in the array should be genres 
                            genres = details[0];
                            // if there is another item in the array it should be director
                            movie.director = details.Length > 1 ? details[1] : "unassigned";
                            // if there is another item in the array it should be run time
                            movie.runningTime = details.Length > 2 ? TimeSpan.Parse(details[2]) : new TimeSpan(0);
                        }

                        sw.WriteLine($"{movie.mediaId},{movie.title},{genres},{movie.director},{movie.runningTime}");
                    }

                    sw.Close();
                    sr.Close();
                    logger.Info("File scrub ended");
                }

                return writeFile;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return "";
        }
    }

    public static class BookFileScrubber
    {
        private static readonly Logger logger = NLogBuilder
            .ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public static string ScrubMovies(string readFile)
        {
            try
            {
                // determine name of writeFile
                var ext = readFile.Split('.').Last();
                var writeFile = readFile.Replace(ext, $"scrubbed.{ext}");
                // if writeFile exists, the file has already been scrubbed
                if (File.Exists(writeFile))
                {
                    // file has already been scrubbed
                    logger.Info("File already scrubbed");
                }
                else
                {
                    // file has not been scrubbed
                    logger.Info("File scrub started");
                    // open write file
                    var sw = new StreamWriter(writeFile);
                    // open read file
                    var sr = new StreamReader(readFile);
                    // remove first line - column headers
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        // create instance of Movie class
                        var movie = new Movie();
                        var line = sr.ReadLine();
                        // look for quote(") in string
                        // this indicates a comma(,) or quote(") in movie title
                        var idx = line.IndexOf('"');
                        var genres = "";
                        if (idx == -1)
                        {
                            // no quote = no comma or quote in movie title
                            // movie details are separated with comma(,)
                            var movieDetails = line.Split(',');
                            movie.mediaId = ulong.Parse(movieDetails[0]);
                            movie.title = movieDetails[1];
                            genres = movieDetails[2];
                            movie.director = movieDetails.Length > 3 ? movieDetails[3] : "unassigned";
                            movie.runningTime = movieDetails.Length > 4
                                ? TimeSpan.Parse(movieDetails[4])
                                : new TimeSpan(0);
                        }
                        else
                        {
                            // quote = comma or quotes in movie title
                            // extract the movieId
                            movie.mediaId = ulong.Parse(line.Substring(0, idx - 1));
                            // remove movieId and first comma from string
                            line = line.Substring(idx);
                            // find the last quote
                            idx = line.LastIndexOf('"');
                            // extract title
                            movie.title = line.Substring(0, idx + 1);
                            // remove title and next comma from the string
                            line = line.Substring(idx + 2);
                            // split the remaining string based on commas
                            var details = line.Split(',');
                            // the first item in the array should be genres 
                            genres = details[0];
                            // if there is another item in the array it should be director
                            movie.director = details.Length > 1 ? details[1] : "unassigned";
                            // if there is another item in the array it should be run time
                            movie.runningTime = details.Length > 2 ? TimeSpan.Parse(details[2]) : new TimeSpan(0);
                        }

                        sw.WriteLine($"{movie.mediaId},{movie.title},{genres},{movie.director},{movie.runningTime}");
                    }

                    sw.Close();
                    sr.Close();
                    logger.Info("File scrub ended");
                }

                return writeFile;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return "";
        }
    }

    public static class AlbumFileScrubber
    {
        private static readonly Logger logger = NLogBuilder
            .ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public static string ScrubMovies(string readFile)
        {
            try
            {
                // determine name of writeFile
                var ext = readFile.Split('.').Last();
                var writeFile = readFile.Replace(ext, $"scrubbed.{ext}");
                // if writeFile exists, the file has already been scrubbed
                if (File.Exists(writeFile))
                {
                    // file has already been scrubbed
                    logger.Info("File already scrubbed");
                }
                else
                {
                    // file has not been scrubbed
                    logger.Info("File scrub started");
                    // open write file
                    var sw = new StreamWriter(writeFile);
                    // open read file
                    var sr = new StreamReader(readFile);
                    // remove first line - column headers
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        // create instance of Movie class
                        var movie = new Movie();
                        var line = sr.ReadLine();
                        // look for quote(") in string
                        // this indicates a comma(,) or quote(") in movie title
                        var idx = line.IndexOf('"');
                        var genres = "";
                        if (idx == -1)
                        {
                            // no quote = no comma or quote in movie title
                            // movie details are separated with comma(,)
                            var movieDetails = line.Split(',');
                            movie.mediaId = ulong.Parse(movieDetails[0]);
                            movie.title = movieDetails[1];
                            genres = movieDetails[2];
                            movie.director = movieDetails.Length > 3 ? movieDetails[3] : "unassigned";
                            movie.runningTime = movieDetails.Length > 4
                                ? TimeSpan.Parse(movieDetails[4])
                                : new TimeSpan(0);
                        }
                        else
                        {
                            // quote = comma or quotes in movie title
                            // extract the movieId
                            movie.mediaId = ulong.Parse(line.Substring(0, idx - 1));
                            // remove movieId and first comma from string
                            line = line.Substring(idx);
                            // find the last quote
                            idx = line.LastIndexOf('"');
                            // extract title
                            movie.title = line.Substring(0, idx + 1);
                            // remove title and next comma from the string
                            line = line.Substring(idx + 2);
                            // split the remaining string based on commas
                            var details = line.Split(',');
                            // the first item in the array should be genres 
                            genres = details[0];
                            // if there is another item in the array it should be director
                            movie.director = details.Length > 1 ? details[1] : "unassigned";
                            // if there is another item in the array it should be run time
                            movie.runningTime = details.Length > 2 ? TimeSpan.Parse(details[2]) : new TimeSpan(0);
                        }

                        sw.WriteLine($"{movie.mediaId},{movie.title},{genres},{movie.director},{movie.runningTime}");
                    }

                    sw.Close();
                    sr.Close();
                    logger.Info("File scrub ended");
                }

                return writeFile;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return "";
        }
    }
}