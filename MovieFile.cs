using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;
using NLog.Web;

namespace MediaLibrary
{
    public class MovieFile
    {
        private static readonly Logger logger = NLogBuilder
            .ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public MovieFile(string movieFilePath)
        {
            filePath = movieFilePath;
            Movies = new List<Movie>();

            // to populate the list with data, read from the data file
            try
            {
                var sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    // create instance of Movie class
                    var movie = new Movie();
                    var line = sr.ReadLine();
                    // first look for quote(") in string
                    // this indicates a comma(,) in movie title
                    var idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        // no quote = no comma in movie title
                        // movie details are separated with comma(,)
                        var movieDetails = line.Split(',');
                        movie.mediaId = ulong.Parse(movieDetails[0]);
                        movie.title = movieDetails[1];
                        movie.genres = movieDetails[2].Split('|').ToList();
                        movie.director = movieDetails[3];
                        movie.runningTime = TimeSpan.Parse(movieDetails[4]);
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
                        movie.genres = details[0].Split('|').ToList();
                        // if there is another item in the array it should be director
                        movie.director = details[1];
                        // if there is another item in the array it should be run time
                        movie.runningTime = TimeSpan.Parse(details[2]);
                    }

                    Movies.Add(movie);
                }

                // close file when done
                sr.Close();
                logger.Info("Movies in file {Count}", Movies.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // public property
        public string filePath { get; set; }
        public List<Movie> Movies { get; set; }

        // constructor is a special method that is invoked
        // when an instance of a class is created

        public void Search(string query)
        {
            filePath = "movies.scrubbed.csv";
            Movies = new List<Movie>();
            var found = new List<Movie>();

            // to populate the list with data, read from the data file
            try
            {
                var sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    foreach (var m in Movies.Where(m => m.title.Contains(query))) found.Add(m);
                    foreach (var m in Movies.Where(m => m.director.Contains(query))) found.Add(m);
                    foreach (var m in Movies.Where(m => m.director.Contains(query))) found.Add(m);
                    foreach (var m in Movies.Where(m => m.genres.Contains(query))) found.Add(m);
                }

                var count = 0;
                foreach (var mov in found)
                {
                    Console.WriteLine(mov.ToString());
                    count++;
                }

                Console.WriteLine($"Movie(s) found: {count}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // public method
        public bool isUniqueTitle(string title)
        {
            if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate movie title {Title}", title);
                return false;
            }

            return true;
        }

        public void AddMovie(Movie movie)
        {
            try
            {
                // first generate movie id
                movie.mediaId = Movies.Max(m => m.mediaId) + 1;
                // if title contains a comma, wrap it in quotes
                var title = movie.title.IndexOf(',') != -1 || movie.title.IndexOf('"') != -1
                    ? $"\"{movie.title}\""
                    : movie.title;
                var sw = new StreamWriter(filePath, true);
                // write movie data to file
                sw.WriteLine(
                    $"{movie.mediaId},{title},{string.Join("|", movie.genres)},{movie.director},{movie.runningTime}");
                sw.Close();
                // add movie details to List
                Movies.Add(movie);
                // log transaction
                logger.Info("Media id {Id} added", movie.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }

    public class BookFile
    {
        private static readonly Logger logger = NLogBuilder
            .ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        // constructor is a special method that is invoked
        // when an instance of a class is created
        public BookFile(string movieFilePath)
        {
            filePath = movieFilePath;
            Movies = new List<Movie>();

            // to populate the list with data, read from the data file
            try
            {
                var sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    // create instance of Movie class
                    var movie = new Movie();
                    var line = sr.ReadLine();
                    // first look for quote(") in string
                    // this indicates a comma(,) in movie title
                    var idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        // no quote = no comma in movie title
                        // movie details are separated with comma(,)
                        var movieDetails = line.Split(',');
                        movie.mediaId = ulong.Parse(movieDetails[0]);
                        movie.title = movieDetails[1];
                        movie.genres = movieDetails[2].Split('|').ToList();
                        movie.director = movieDetails[3];
                        movie.runningTime = TimeSpan.Parse(movieDetails[4]);
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
                        movie.genres = details[0].Split('|').ToList();
                        // if there is another item in the array it should be director
                        movie.director = details[1];
                        // if there is another item in the array it should be run time
                        movie.runningTime = TimeSpan.Parse(details[2]);
                    }

                    Movies.Add(movie);
                }

                // close file when done
                sr.Close();
                logger.Info("Movies in file {Count}", Movies.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // public property
        public string filePath { get; set; }
        public List<Movie> Movies { get; set; }

        // public method
        public bool isUniqueTitle(string title)
        {
            if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate movie title {Title}", title);
                return false;
            }

            return true;
        }

        public void AddMovie(Movie movie)
        {
            try
            {
                // first generate movie id
                movie.mediaId = Movies.Max(m => m.mediaId) + 1;
                // if title contains a comma, wrap it in quotes
                var title = movie.title.IndexOf(',') != -1 || movie.title.IndexOf('"') != -1
                    ? $"\"{movie.title}\""
                    : movie.title;
                var sw = new StreamWriter(filePath, true);
                // write movie data to file
                sw.WriteLine(
                    $"{movie.mediaId},{title},{string.Join("|", movie.genres)},{movie.director},{movie.runningTime}");
                sw.Close();
                // add movie details to List
                Movies.Add(movie);
                // log transaction
                logger.Info("Media id {Id} added", movie.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }

    public class AlbumFile
    {
        private static readonly Logger logger = NLogBuilder
            .ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        // constructor is a special method that is invoked
        // when an instance of a class is created
        public AlbumFile(string movieFilePath)
        {
            filePath = movieFilePath;
            Movies = new List<Movie>();

            // to populate the list with data, read from the data file
            try
            {
                var sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    // create instance of Movie class
                    var movie = new Movie();
                    var line = sr.ReadLine();
                    // first look for quote(") in string
                    // this indicates a comma(,) in movie title
                    var idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                        // no quote = no comma in movie title
                        // movie details are separated with comma(,)
                        var movieDetails = line.Split(',');
                        movie.mediaId = ulong.Parse(movieDetails[0]);
                        movie.title = movieDetails[1];
                        movie.genres = movieDetails[2].Split('|').ToList();
                        movie.director = movieDetails[3];
                        movie.runningTime = TimeSpan.Parse(movieDetails[4]);
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
                        movie.genres = details[0].Split('|').ToList();
                        // if there is another item in the array it should be director
                        movie.director = details[1];
                        // if there is another item in the array it should be run time
                        movie.runningTime = TimeSpan.Parse(details[2]);
                    }

                    Movies.Add(movie);
                }

                // close file when done
                sr.Close();
                logger.Info("Movies in file {Count}", Movies.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        // public property
        public string filePath { get; set; }
        public List<Movie> Movies { get; set; }

        // public method
        public bool isUniqueTitle(string title)
        {
            if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate movie title {Title}", title);
                return false;
            }

            return true;
        }


        public void AddMovie(Movie movie)
        {
            try
            {
                // first generate movie id
                movie.mediaId = Movies.Max(m => m.mediaId) + 1;
                // if title contains a comma, wrap it in quotes
                var title = movie.title.IndexOf(',') != -1 || movie.title.IndexOf('"') != -1
                    ? $"\"{movie.title}\""
                    : movie.title;
                var sw = new StreamWriter(filePath, true);
                // write movie data to file
                sw.WriteLine(
                    $"{movie.mediaId},{title},{string.Join("|", movie.genres)},{movie.director},{movie.runningTime}");
                sw.Close();
                // add movie details to List
                Movies.Add(movie);
                // log transaction
                logger.Info("Media id {Id} added", movie.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}