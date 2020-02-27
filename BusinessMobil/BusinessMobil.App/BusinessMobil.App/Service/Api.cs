using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusinessMobil.App.Service
{
    public class Api
    {
        private string urlBase = "http://joselelu-001-site2.etempurl.com/api";
        public Api()
        {
        }

        public async Task<Response> GetListRespondeAsync<T>(string parameter)
        {
            try
            {
                var client = new HttpClient();
                //{
                //    BaseAddress = new Uri(urlBase)
                //};

                var url = $"{parameter}";
                var response = client.GetAsync($"{urlBase}/{url}").Result;
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
                //var response = client.GetAsync($"{url}/{parameter}");
                //var data = JsonConvert.DeserializeObject<List<T>>(response);

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> GetListRespondeAsync<T>(string parameter, Token token)
        {
            try
            {
                var client = new HttpClient();
                //{
                //    BaseAddress = new Uri(urlBase)
                //};

                var url = $"{parameter}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.access_token);
                var response = client.GetAsync($"{urlBase}/{url}").Result;
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
                //var response = client.GetAsync($"{url}/{parameter}");
                //var data = JsonConvert.DeserializeObject<List<T>>(response);

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> GetrespondeAsync<T>(string parameter, Token token)
        {
            try
            {
                var client = new HttpClient();
                var url = $"{parameter}";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.access_token);
                var response = client.GetAsync($"{urlBase}/{url}").Result;
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
                //var response = client.GetAsync($"{url}/{parameter}");
                //var data = JsonConvert.DeserializeObject<List<T>>(response);

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> PostRespondeAsync<T>(string parameter, T data)
        {
            try
            {
                var client = new HttpClient();
                //{
                //    BaseAddress = new Uri(urlBase)
                //};

                var url = $"{parameter}";
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{urlBase}/{url}", content).Result;
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = result
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> PostRespondeAsync<T>(string parameter, T data, Token token)
        {
            try
            {
                var client = new HttpClient();
                //{
                //    BaseAddress = new Uri(urlBase)
                //};
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.access_token);
                var url = $"{parameter}";
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{urlBase}/{url}", content).Result;
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Result = result
                };

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
                //var response = client.GetAsync($"{url}/{parameter}");
                //var data = JsonConvert.DeserializeObject<List<T>>(response);

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
