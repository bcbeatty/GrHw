using System;
using System.Linq;
using Autofac;
using GrHw.Client.Business;
using GrHw.Client.Domain;
using GrHw.Client.Module;
using GrHw.Framework;


namespace ImportPerson
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var delimeter = ParseCommandLine(args, "-d");
            if (delimeter is null)
            {
                delimeter = " ";
            }
            var filename = ParseCommandLine(args, "-f");


            var builder = new ContainerBuilder();
            builder.RegisterModule<ClientAutofacModule>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                try
                {

                    var processor = scope.Resolve<IBatchProcessor<Person>>();

                    var lines = processor.Load(filename);
                    var people = processor.Parse(lines, delimeter[0]);
                    processor.Save(people);
                    var reports = processor.GetReports(people, delimeter[0]);
                    foreach (var report in reports)
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine(report);
                    }
                }
                catch (Exception ex)
                {
                    // write exception to log

                    Console.WriteLine("Error Occurred, Check Application Log for more details." + ex);

                }
            }

            Environment.Exit(0);
        }

        private static string ParseCommandLine(string[] args, string parameter)
        {
            return args.FirstOrDefault(a => a.Split("=")[0] == parameter)?.Split("=")?[1];

        }
    }
}
