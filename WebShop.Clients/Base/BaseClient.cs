using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WebShop.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Client;

        protected abstract string ServiceAddress { get; set; }

        protected BaseClient(IConfiguration configuration)
        {
            // Создаем экземпляр клиента
            Client = new HttpClient
            {
                // Базовый адрес, на котором будут хостится сервисы
                BaseAddress = new Uri(configuration["ClientAdress"])
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            // Устанавливаем хедер, который будет говорит серверу, чтобы он отправлял данные в формате json
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


    }
}
