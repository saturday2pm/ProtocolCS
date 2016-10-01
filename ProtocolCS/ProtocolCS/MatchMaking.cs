using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolCS
{
    /// <summary>
    /// 클라이언트가 서버로 보내는 매칭 요청
    /// </summary>
    public class JoinQueue : PacketBase
    {
    }

    /// <summary>
    /// 클라이언트가 서버로 보내는 매칭 취소 요청
    /// </summary>
    public class LeaveQueue : PacketBase
    {

    }

    /// <summary>
    /// 매치가 성사되면 서버가 클라에게 보내주는 패킷
    /// </summary>
    public class MatchSuccess : PacketBase
    {
        /// <summary>
        /// 게임 서버 주소
        /// </summary>
        public string gameServerAddress { get; set; }

        /// <summary>
        /// 게임 서버에 연결한 후, 인증하기 위한 토큰
        /// </summary>
        public string matchToken { get; set; }
    }
}
