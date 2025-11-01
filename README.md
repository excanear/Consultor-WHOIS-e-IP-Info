# Consultor WHOIS e IP Info (C#)

Um projeto completo em .NET 6.0 para consultar informações WHOIS, geolocalização de IP, dados RDAP e registros DNS.

## 🚀 Funcionalidades

- **WHOIS**: Consulta informações de domínios e IPs via protocolo WHOIS (porta 43)
- **Geolocalização IP**: Consulta localização geográfica via ip-api.com
- **RDAP**: Consulta dados RDAP (Registration Data Access Protocol) via rdap.org
- **DNS**: Consulta registros A, MX e reverse PTR lookup

## 📋 Comandos Disponíveis

- `whois <dominio>` — consulta WHOIS de um domínio ou IP
- `ip <endereco-ip>` — consulta geolocalização via ip-api.com
- `rdap <endereco-ip>` — consulta RDAP (rdap.org)
- `dns <dominio>` — consulta registros A e MX

## 🛠️ Como Usar

### Instalação e Build

```powershell
cd "c:\Users\Henry\OneDrive\Área de Trabalho\Consultor WHOIS e IP Info\WhoisIpConsultant"
dotnet restore
dotnet build
```

### Exemplos de Uso

**1. Consulta WHOIS:**
```powershell
dotnet run -- whois google.com
dotnet run -- whois 8.8.8.8
```

**2. Geolocalização de IP:**
```powershell
dotnet run -- ip 8.8.8.8
dotnet run -- ip 1.1.1.1
```

**3. Consulta RDAP:**
```powershell
dotnet run -- rdap 8.8.8.8
dotnet run -- rdap 208.67.222.222
```

**4. Consulta DNS:**
```powershell
dotnet run -- dns google.com
dotnet run -- dns microsoft.com
```

**5. Modo Interativo:**
```powershell
dotnet run
```
No modo interativo, digite os comandos diretamente:
```
comando> whois example.com
comando> ip 8.8.8.8
comando> dns google.com
comando> exit
```

## 🔧 Estrutura do Projeto

- `Program.cs` - Interface CLI e modo interativo
- `WhoisClient.cs` - Cliente WHOIS para consulta de domínios/IPs
- `IpInfoClient.cs` - Cliente para geolocalização e RDAP
- `DnsHelper.cs` - Helper para consultas DNS (A, MX, PTR)

## 📦 Dependências

- **.NET 6.0** (compatível com .NET 6.0.36)
- **DnsClient 1.7.0** - Biblioteca para consultas DNS

## ⚠️ Observações

- **ip-api.com**: Serviço gratuito para geolocalização com limite de requisições
- **WHOIS**: Suporte básico para TLDs comuns (.com, .net, .org, .br, .io)
- **Timeouts**: Configurados para evitar travamentos em consultas lentas
- **Encoding**: Suporte a UTF-8 para caracteres especiais

## 🌟 Possíveis Melhorias

- Adicionar mais servidores WHOIS para TLDs específicos
- Implementar cache local para consultas repetidas
- Adicionar interface gráfica (WPF/Windows Forms)
- Suporte a IPv6 completo
- Exportação de resultados para JSON/CSV
- Testes unitários automatizados

## 📝 Status do Build

✅ Build bem-sucedido com .NET 6.0  
✅ Todas as funcionalidades testadas e funcionando  
✅ Compatível com Windows PowerShell  

---
*Desenvolvido em C# - Novembro 2025*
