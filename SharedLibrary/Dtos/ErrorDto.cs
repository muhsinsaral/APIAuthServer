using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.Dtos
{
    public class ErrorDto
    {
        public List<String> Errors { get; }
        public bool IsShow { get; }
        public ErrorDto()
        {
            Errors = new List<String>();
        }
        public ErrorDto(string error, bool isShow) : this()
        {
            Errors.Add(error);
            IsShow = isShow;
        }
        public ErrorDto(List<string> errors, bool isShow) : this()
        {
            Errors = errors;
            IsShow = isShow;
        }

    }
}