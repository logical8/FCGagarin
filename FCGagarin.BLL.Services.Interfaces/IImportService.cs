using System.Collections.Generic;
using FCGagarin.DAL.Entities;

namespace FCGagarin.BLL.Services.Interfaces
{
    public interface IImportService
    {
        /// <summary>
        /// Get and update rounds from lfl.ru
        /// </summary>
        /// <param name="clubId">ClubId from lfl.ru</param>
        /// <param name="season">Format: "2017-2018"</param>
        void ImportRounds(int clubId, string season);
    }
}