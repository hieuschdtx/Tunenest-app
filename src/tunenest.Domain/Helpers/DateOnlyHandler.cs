using System.Data;
using Dapper;

namespace tunenest.Domain.Helpers
{
    public class DateOnlyHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.Value = value.ToDateTime(new TimeOnly(0, 0));
        }

        public override DateOnly Parse(object value)
        {
            return DateOnly.FromDateTime((DateTime)value);
        }
    }
}
