namespace Wizmail
{
    using System.Linq;
    using AutoMapper;
    using SimpleHttpServer;
    using SimpleMVC;
    using Wizmail.BindingModels;
    using Wizmail.Models;
    using Wizmail.ViewModels;

    public class StartUp
    {
        public static void Main()
        {
            ConfigureMapper();
            HttpServer server = new HttpServer(8081, RoutesTable.Routes);
            MvcEngine.Run(server, "Wizmail");
        }

        private static void ConfigureMapper()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<RegisterUserBm, User>();
                expression.CreateMap<NewMailBm, Email>();
                expression.CreateMap<Email, DetailedMailVm>()
                    .ForMember(vm => vm.Recipients, 
                    configurationExression => 
                    configurationExression.MapFrom(m => string.Join("; ", m.Recipients.Select(u => u.Username + 
                    "@wizmail.bg"))))
                    .ForMember(vm => vm.Sender, 
                    configurationExpression =>
                    configurationExpression.MapFrom(m => m.Sender.Username + "@wizmail.bg"));
                expression.CreateMap<Email, EmailVm>()
                    .ForMember(vm => vm.IsRead, 
                    configurationExpression =>
                    configurationExpression.MapFrom(e => e.Flag == Flag.Read ? true : false));
            });
        }
    }
}
