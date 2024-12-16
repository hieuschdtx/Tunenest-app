using tunenest.Domain.Extensions;

namespace tunenest.Domain.Commons.Interfaces.Commands
{
    public class CommandBase<TEntityId> : ICommand
    {
        public TEntityId id { get; set; }

        public CommandBase()
        {
            if (typeof(TEntityId) == typeof(Guid))
            {
                this.id = (TEntityId)(object)Guid.NewGuid();
            }
            else
            {
                this.id = default!;
            }
        }

        public CommandBase(TEntityId id)
        {
            this.id = id;
        }
    }

    public class CommandBase<TResponse, TEntityId> : ICommand<TResponse>
    {
        public Guid user_id { get; private set; }
        public TEntityId id { get; private set; }

        public CommandBase()
        {
            if (typeof(TEntityId) == typeof(Guid))
            {
                this.id = (TEntityId)(object)Guid.NewGuid();
            }
            else
            {
                this.id = default!;
            }
        }

        public CommandBase(TEntityId id)
        {
            this.id = id;
        }

        public void SetUserId(Guid userId)
        {
            this.user_id = userId;
        }

        public void SetId(string id)
        {
            if (typeof(TEntityId) == typeof(Guid))
            {
                this.id = (TEntityId)(object)id.ToGuid();
            }
            else if (typeof(TEntityId) == typeof(long))
            {
                this.id = (TEntityId)(object)long.Parse(id);
            }
            else if (typeof(TEntityId) == typeof(string))
            {
                this.id = (TEntityId)(object)id;
            }
            else
            {
                throw new NotSupportedException($"Type {typeof(TEntityId)} is not supported");
            }
        }
    }
}
