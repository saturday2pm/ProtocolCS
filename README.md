ProtocolCS
====

서버 /클라이언트 /인공지능 프로젝트에서 공통적으로 사용할 프로토콜 정의.


Usage
----
```cs
using ProtocolCS;

// RECEIVE
void OnMessage(string data) {
  var packet = Serializer.ToObject(data);
  
  if (packet is StartGame) { /* ... */ }
}

// SEND
void SendPacket<T>(T data) {
  var json = Serializer.ToJson(data);
  
  Send(json);
}
```
 
 
  
 
 
 
 
