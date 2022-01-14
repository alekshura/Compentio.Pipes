# Compentio.Pipes
Provides interprocess communication between a pipe server and one or more pipe clients.

## Can be used
- Instead of WCF on one machine or over local network for duplex interprocess communication. Since .NET Core and .NET 6 do not offer server-side support for hosting WCF (actually it is recommended to [migrate WCF to gRPC](https://docs.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/migrate-wcf-to-grpc) or use [CoreWCF](https://github.com/CoreWCF/CoreWCF)) NamedPipes can be used for communication.
 - Desktop applications: for example to notify GUI application from batch service worker (e.g. Windows Service). It is much faster than creating HTTP channel for such communication.
 - You can consider writing scaled application with async communication using NamedPipes.
