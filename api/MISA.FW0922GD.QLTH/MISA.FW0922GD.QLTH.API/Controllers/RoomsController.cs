using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FW0922GD.QLTH.BL.BaseBL;
using MISA.FW0922GD.QLTH.Common.Entities;

namespace MISA.FW0922GD.QLTH.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class RoomsController : BasesController<Room>
    {
        #region Field

        private IBaseBL<Room> _baseBL;

        #endregion

        #region Constructor

        public RoomsController(IBaseBL<Room> baseBL) : base(baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method



        #endregion

    }
}
