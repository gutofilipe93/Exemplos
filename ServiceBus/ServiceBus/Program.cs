using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Envia para o serviceBus
                var _serviceBusClient = new ServiceBusClient("Endpoint=sb://ideverservicebushomo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=bV6ayct6klBCMRNv9vqkOze3XVa3JZaIWxpKf0qMeM0=");
                ServiceBusSender sender = _serviceBusClient.CreateSender("cupomprocessado");
                var formPdv = new FormPdv
                {
                    FormId = 23,
                    Answers = new List<AnswerPdv>
                    {
                        new AnswerPdv
                        {
                            Answer = "false",
                            QuestionId = 49
                        },
                        new AnswerPdv
                        {
                            Answer = "5",
                            QuestionId = 50
                        }
                    }
                };

                var json = JsonConvert.SerializeObject(new
                {
                    PersonId = 127124,
                    ClientId = 10,
                    CouponDate = "2021-12-14 12:09:14.860",
                    CouponId = 68428845,
                    CompanyId = 127122,
                    OrderVoucher = "",
                    Vouchers = "",
                    //FormsPdv = new List<FormPdv> { formPdv}
                });
                Console.WriteLine("Começou");
                await sender.SendMessageAsync(new ServiceBusMessage(json)) ;
                Console.WriteLine("Finalizou");
               // Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ReadKey();
            }
        }
    }

    public class FormPdv
    {
        public int FormId { get; set; }
        public List<AnswerPdv> Answers { get; set; }

    }
    public class AnswerPdv
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
