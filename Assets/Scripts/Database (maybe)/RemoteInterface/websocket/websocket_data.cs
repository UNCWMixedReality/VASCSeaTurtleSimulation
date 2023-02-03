/*

    Daniel Vaughn
    UNCW VR_Research_Team

    websocket_data.cs

*/

using System.Collections;
using System.Collections.Generic;

// Bit masks for decoding WebSocket message
public class websocketDecoder
{
    public const int _MASK_FIN_BIT = 128;
    public const int _MASK_OPCODE = 15;
    public const int _MASK_RSV1 = 64;
    public const int _MASK_RSV2 = 32;
    public const int _MASK_RSV3 = 16;
    public const int _MASK_PAYLOAD_LENGTH = 127;
    public const int _MASK_PAYLOAD_IS_16BIT = 126;
    public const int _MASK_PAYLOAD_IS_64BIT = 127;
}

// Websocket OPCODE types
public class websocketOpcodes
{
    public const short _CONTINUATION = 0x0;
    public const short _TEXT = 0x1;
    public const short _BINARY = 0x2;
    // 0x3 - 0x7 UNUSED
    public const short _CLOSE = 0x8;
    public const short _PING = 0x9;
    public const short _PONG = 0xA;
    // 0xB - 0xF UNUSED
}

// A websocket message structure
public class websocketMessage
{
    public int opcode;
    public long payload_length;
    public byte[] rsv = new byte[3];
    public byte[] data_mask = new byte[4];
    public int data_offset = 2;
    public List<byte> data = new List<byte>();
    public List<byte> decoded = new List<byte>();
}

// Response handshake
public class websocketHandshake
{
    public const string _ES_EOL = "\r\n";
    // ID as defined in RFC6455
    public const string _WS_ID = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
}
