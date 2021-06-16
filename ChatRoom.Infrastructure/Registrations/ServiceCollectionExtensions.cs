using ChatRoom.Core.Commands;
using ChatRoom.Core.DataStore;
using ChatRoom.Core.Events;
using ChatRoom.Core.Queries;
using ChatRoom.Core.Queries.QueryArgs;
using ChatRoom.Core.Repositories;
using ChatRoom.Core.Services;
using ChatRoom.Infrastructure.DataStore;
using ChatRoom.Infrastructure.Repositories;
using ChatRoom.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.Infrastructure.Registrations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityDataStore), typeof(EntitiesDataStore));
            services.AddScoped(typeof(IEventsDataStore), typeof(EventsDataStore));
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped(typeof(IEventRepository<>), typeof(ChatRoomEventsRepository<>));
            services.AddScoped<IQuery<ShowAllEventsArgs>, QueryShowAllEvents>();
            services.AddScoped<IQuery<ShowAllAggregatedArgs>, QueryShowAllAggregated>();
            services.AddScoped<IQuery<StatusByMinuteArgs>, QueryStatusByMinute>();
            services.AddScoped<IQuery<AggregateByHourArgs>, QueryAggregateResultsByHour>();
            services.AddScoped<IQuery<AggregateByMinuteArgs>, QueryAggregateResultsByMinute>();
            services.AddScoped<IQuery<StatusByHourArgs>, QueryStatusByHour>();
            services.AddScoped<IChatCommand<EnterChatRoomEvent>, EnterChatRoomCommand>();
            services.AddScoped<IChatCommand<LeaveChatRoomEvent>, LeaveChatRoomCommand>();
            services.AddScoped(typeof(IChatRoomEventProcessorService<>),
                typeof(ChatRoomEventProcessorService<>));
            services.AddScoped(typeof(IQueryService<>), typeof(QueryService<>));
            return services;
        }
    }
}
