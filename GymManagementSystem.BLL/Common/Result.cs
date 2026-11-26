using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.BLL.Common
{

    // record --> do not change object attributes as we go 

    // that one for services that do not return data as create or update just bool is returned
    public record Result(bool Success , string? error = null ,ResultKind Kind = ResultKind.OK)
    {
        public static Result OK() => new(true);
        public static Result Fail(string error, ResultKind Kind = ResultKind.Conflict) => new(false, error, Kind);
        public static Result NotFound(string error = "Not Found") => new(false, error, ResultKind.NotFound);
        public static Result Validation(string error) => new(false, error, ResultKind.ValidationFailed);

    }

    // that one used for services that return data as getbyid , get all and so on
    public record Result<T>(bool Success, T? Value, string? error = null, ResultKind Kind = ResultKind.OK)
    {

        // that one i care about to return data
        public static Result<T> OK(T value) => new(true,value);
        // that one i do not care to return data
        public static Result<T> Fail(string error, ResultKind Kind = ResultKind.Conflict) => new(false,default ,error, Kind);
        public static Result<T> NotFound(string error = "Not Found") => new(false, default ,error, ResultKind.NotFound);


    }

}
