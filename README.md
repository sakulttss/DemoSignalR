# Demo SignalR

## Setup SignalR
ให้เลือกรูปแบบการใช้ SignalR จาก 2 วิธีด้านล่าง โดยเลือกอย่างใดอย่างหนึ่ง
### 🅰️กรณีใช้ SignalR emulator
1. หากไม่เคยใช้ SignalR emulator ในเครื่องมาก่อน จะต้องทำการติดตั้งด้วยคำสั่งด้านล่าง แต่ถ้าเคยทำแล้วให้ข้ามไปข้อถัดไป
```bash
dotnet tool install  -g Microsoft.Azure.SignalR.Emulator
dotnet tool update -g Microsoft.Azure.SignalR.Emulator
```
2. ใน repo นี้จะมีโฟเดอร์ที่เตรียมเรื่อง SignalR emulator ไว้อยู่แล้ว ให้เข้าไปในโฟเดอร์ `src/SignalREmulator` แล้วรันคำสั่งด้านล่าง
```bash
asrs-emulator start
```
3. หากเห็น ConnectionString ขึ้นมา ก็แสดงว่าเครื่องเราพร้อมใช้งาน SignalR emulator แล้ว ซึ่งโดยปรกติตัว emulator จะมี endpoint เป็น `Endpoint=http://localhost;Port=8888;AccessKey=ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGH;Version=1.0;`

### 🅱️กรณีใช้ Azure SignalR Service
1. ให้เข้าไปที่ Azure Portal แล้วไปก๊อปปี้ ConnectionString ออกมา
2. เปิดไฟล์ `src/Host/appsettings.json` แล้วให้นำค่าที่ก๊อปปี้ไว้ไปแทนที่ `Azure:SignalR:ConnectionString` ในไฟล์ `appsettings.json`
```json
"Azure": {
    "SignalR": {
        "ConnectionString": "เอามาแทนที่ตรงนี้"
    }
}
```
3. เปิดไฟล์ `src/Host/Program.cs` แล้วแก้บรรทัดที่ 7 ให้เป็นตามด้านล่าง เพียงเท่านี้เราก็พร้อมใช้งาน Azure SignalR Service แล้ว
```csharp
builder.Services.AddSignalR().AddAzureSignalR();
```

## วิธีรันโปรเจค
1. เรามีโฟเดอร์ Host เป็นตัว hosting SignalR ดังนั้นเราจะต้องรันตัวนี้ก่อน โดยเข้าไปในโฟเดอร์ `src/Host` แล้วรันคำสั่งด้านล่าง แล้วตัวเว็บจะถูกเปิดขึ้นมาที่ `http://localhost:5215/index.html` ซึ่งเราสามารถเข้าไปใช้งานเว็บได้เลย Chat
```bash
dotnet run
```
2. เรามี Client อีก 2 ตัวนั่นคือ `JavascriptClient` และ `ConsoleClient` ซึ่งต้องการรันตัวไหนก็เข้าไปในโฟเดอร์นั้นๆ แล้วเรียกใช้คำสั่งด้านล่างเพื่อทำการรันได้เลย
```bash
dotnet run
```