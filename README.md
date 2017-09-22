# nZadarma
[Zadarma](https://zadarma.com/) API for .NET45

[nZadarma nuget package](https://www.nuget.org/packages/nZadarma/)

## Usage example
```csharp
var api = new ZadarmaApi("your key", "your secret key");
var balance = await api.Balance();
var sips = await api.Sips();
var callback = await.Callback("number from", "number to");
```
