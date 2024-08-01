using System;
using upword.Api.Dtos;
using upword.Api.Entities;

namespace upword.Api.Mapping
{
    public static class UserWordMapping
    {
        public static UserWord ToEntity(this CreateUserWordDto userWordDto, string newId)
        {
            return new UserWord
            {
                Id = newId,
                UserId = userWordDto.UserId,
                WordId = userWordDto.WordId
            };
        }

        public static UserWordDto ToDto(this UserWord userWord)
        {
            return new UserWordDto
            {
                Id = userWord.Id,
                UserId = userWord.UserId,
                WordId = userWord.WordId
            };
        }

        public static UserWord ToEntity(this UpdateUserWordDto userWordDto, string id)
        {
            return new UserWord
            {
                Id = id,
                UserId = userWordDto.UserId,
                WordId = userWordDto.WordId
            };
        }
    }
}
