using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestGraphQLWithHotChocolate
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=DemoGraphQL"));

            services.AddInMemorySubscriptions();
            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddServices(sp)
                .Create());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets();
            app.UseGraphQL();
        }


        public class Subscription
        {

            [SubscribeAndResolve]
            public async ValueTask<IAsyncEnumerable<string>> OnOpportunityCreated([Service] ITopicEventReceiver topicEventReceiver)
            {
                return await topicEventReceiver.SubscribeAsync<string, string>("opportunities");
            }

            [SubscribeAndResolve]
            public async IAsyncEnumerable<string> OnMessagesAsync()
            {
                yield return "1";
                await Task.Delay(2000);
                yield return "2";
                await Task.Delay(2000);
                yield return "3";
                await Task.Delay(2000);
                yield return "4";
            }
        }


        public class Mutation
        {
            public async Task<Guid> CreateOpportunity(
                string subject,
                Guid accountId,
                Guid userId,
                [Service] ApplicationDbContext context,
                [Service] ITopicEventSender topicEventSender)
            {
                var opportunity = new Opportunity()
                {
                    AccountId = accountId,
                    Subject = subject,
                    OwnerId = userId
                };
                context.Opportunities.Add(opportunity);
                await context.SaveChangesAsync();
                await topicEventSender.SendAsync("opportunities", opportunity.Id.ToString());
                return opportunity.Id;
            }
        }


        public class Query
        {
            [UseSelection]
            [UseFiltering]
            [UseSorting]
            public IEnumerable<Opportunity> GetOpportunitities([Service] ApplicationDbContext context)
                    => context.Opportunities;
        }
    }
}
