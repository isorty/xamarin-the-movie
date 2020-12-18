using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheMovie.Models;
using Xamarin.Forms;
using System.Net.Http.Headers;
using System.IO;
using System.Globalization;
using System.Diagnostics;

[assembly: Dependency(typeof(TheMovie.Services.MoviesService))]
namespace TheMovie.Services
{
    public class MoviesService
    {
        private const string baseApiUrl = "http://10.0.2.2:5000/api";

        private HttpClient httpClient;

        public MoviesService()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
            };
            httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        ~MoviesService()
        {
            httpClient.Dispose();
        }

        public async Task<List<ShortMovie>> GetMoviesByGenre(int id)
        {            
            var restUrl = $"{baseApiUrl}/genre/{id}";
            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<List<ShortMovie>>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }                    
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }            

            return new List<ShortMovie>() { new ShortMovie() };
        }               
        
        public async Task<Movie> GetMovieById(int id)
        {
            var restUrl = $"{baseApiUrl}/movie/{id}";
            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<Movie>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }            

            return new Movie();
        }

        public async Task<List<Genre>> GetGenres()
        {            
            var restUrl = $"{baseApiUrl}/genre";
            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<List<Genre>>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError(ex);
            }            

            return new List<Genre>() { new Genre() };
        }

        private void ReportError(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }    
}