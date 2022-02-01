using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Homework5._1
{
    class Program
    {
        static void Main(string[] args)
        {
            var listUsers = @"https://reqres.in/api/users?page=2";
            var singleUser = @"https://reqres.in/api/users/2";
            var notFound = @"https://reqres.in/api/users/23";
            var listResource = @"https://reqres.in/api/unknown";
            var resourceNotFound = @"https://reqres.in/api/unknown/23";
            var create = @"https://reqres.in/api/users";

            Console.WriteLine("GET");
            GetAsync(listUsers).GetAwaiter().GetResult();
            GetAsync(singleUser).GetAwaiter().GetResult();
            GetAsync(notFound).GetAwaiter().GetResult();
            GetAsync(listResource).GetAwaiter().GetResult();
            GetAsync(resourceNotFound).GetAwaiter().GetResult();
            Console.WriteLine("POST");
            PostAsync(create).GetAwaiter().GetResult();
            Console.WriteLine("PUT");
            PutAsync(singleUser).GetAwaiter().GetResult();
            Console.WriteLine("PATCH");
            PatchAsync(singleUser).GetAwaiter().GetResult();
            Console.WriteLine("DELETE status code:");
            DeleteAsync(singleUser).GetAwaiter().GetResult();

        }

        static async Task GetAsync(string path)
        {


            using (var httpClient = new HttpClient())
            {
                var task = await httpClient.GetAsync(path);

                if (task.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"error occured by path {path}. Status Code: {task.StatusCode}");

                }
                Console.WriteLine(task.StatusCode);

            }
        }
        static async Task PostAsync(string path)
        {
            using (var httpClient = new HttpClient())
            {
                var newUser = new PostUser()
                {
                    Name = "morpheus",
                    Job = "leader"
                };
                var serializedPostUser = JsonConvert.SerializeObject(newUser);

                var post = await httpClient.PostAsync(
                    path,
                    new StringContent(serializedPostUser, Encoding.UTF8, "aplication/json"));

                Console.WriteLine(await post.Content.ReadAsStringAsync());

            }
        }

        static async Task PutAsync(string path)
        {
            var updatedUser = new PostUser()
            {
                Name = "dasha"
            };
            var serializedUser = JsonConvert.SerializeObject(updatedUser);
            using (var httpClient = new HttpClient())
            {
                var put = await httpClient.PutAsync(path, new StringContent(serializedUser, Encoding.UTF8, "aplication/json"));
                Console.WriteLine("Put request is successful? " + put.IsSuccessStatusCode);
            }

        }

        static async Task PatchAsync(string path)
        {
            var updatedUser = new PostUser()
            {
                Name = "dasha"
            };
            var serializedUser = JsonConvert.SerializeObject(updatedUser);
            using (var httpClient = new HttpClient())
            {
                var patch = await httpClient.PatchAsync(path, new StringContent(serializedUser, Encoding.UTF8, "aplication/json"));
                Console.WriteLine(await patch.Content.ReadAsStringAsync());
            }

        }

        static async Task DeleteAsync(string path)
        {
            using (var httpClient = new HttpClient())
            {
                var delete = await httpClient.DeleteAsync(path);
                Console.WriteLine(delete.StatusCode);
            }

        }
    }
}
