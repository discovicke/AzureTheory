Azure Blob Storage är en Microsoft tjänst för att lagra objekt i molnet. Det är optimerat för att lagra stora mängder av ostrukterad data, och passar bra för att lagra exempelvis bilder, dokument, videor och loggfiler. Det passar bra när man vill streama video/ljud, lagra säkerhetskopior eller analysdata bland annat, då Blob Storage kan leverera bilder eller dokument direkt till webbläsaren. Blob Storage hanterar alla typer av binär data (en BLOB = Binary Large Object).

En användare eller klientprogram kan komma åt objekt i Blob Storage via HTTP/HTTPS-anrop. Objekten i Blob Storage är tillgängliga via Azure Storage REST API, Azure PowerShell, Azure CLI eller ett Azure Storage klientbibliotek, vilka finns tillgängliga för flera olika programmeringsspråk, bland annat för .NET.

Fördelar med att använda Blob Storage är bland annat en hög tillgänglighet för användaren, låga kostnader eftersom lagring sker i olika nivåer, samt att det finns inbyggda säkerhetsfunktioner.

Om vi tänker att  din kod i Program.cs är kocken som bestämmer vad som ska serveras, så är Azure Blob Storage den gigantiska lagerlokalen där du slänger in allt som är för stort för att få plats på skärbrädan.

För att förstå mer hur strukturen ser så kan vi tänka på, Om Blob Storage är det stora externa lagret, behöver vi ett system för att hitta rätt hylla och rätt låda. 
Innan du går in i lagret måste du förstå hierarkin. Det är inte bara en stor hög med saker, utan det är uppdelat i tre nivåer

- *Storage account* (Restaurangkedjan) Detta är själva kontot i Azure. Det är     paraplyet för allt ditt lagringsutrymme.

- *Container* (Rummet/Kylen) 
Inuti ditt konto skapar du "Containers". Tänk på det som olika rum i lagret – ett för "Bilder", ett för "Loggar", och ett för "Menyer". Du kan ha olika säkerhetsregler för olika rum (t.ex. att "Meny-rummet" är öppet för alla, men "Kvitto-rummet" är låst).

- *Blob* (Själva råvaran): Detta är den enskilda filen, t.ex. kalle_anka_profilbild.jpg.

![new blob.png](new%20blob.png)



---
## Innan du kan börja skriva kod som faktiskt pratar med Azure, behöver du din Connection String. Den hittar du i Azure Portal:
[ Länk till Azure Portal](https://portal.azure.com/) 

Gå till ditt Storage Account.

Leta efter Access keys i menyn till vänster.

Klicka på Show vid "Connection string" och kopiera den.

**Varning :** Dela aldrig den strängen på GitHub eller med någon annan – det är huvudnyckeln till hela ditt lager!
Vill du ha ett enkelt kodexempel på hur du laddar upp en textsträng som en fil till din container?

## För att komma igång med Azure blob storage
- installera nuget package för.
- *i Vs Code* i terminalen kör dotnet add package Azure.Storage.Blobs
- *i visual studio* ![Nuget guide.png](Nuget%20guide.png)
- *Kolla i .csproj filen* 
    <ItemGroup>
    <PackageReference Include="Azure.Storage.Blobs" Version="12.xx.x" />
    </ItemGroup>
- *i program.cs* Lägg till *using* högst upp *using Azure.Storage.Blobs;*.


## Hur man får igång Blob Storage på Azure Portal

Gå in på portalen och sök efter "Storage Account" (INTE classic), klicka in på det.
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/110a203b-8613-4cc2-98d1-c6a5b9f6a62b" />

Klicka på "Create":
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/70bbfe6f-49bc-492b-a1cf-d6eb7fd9260a" />

Fyll i informationen, här är det jag rekommenderar. (OBS: om något av alternativen är otydliga går det att hålla musen över "info box" symbolen på höger om titeln för fältet, så finns det en läs mer länk att utforska.

<img width="1920" height="1165" alt="screencapture-portal-azure-2026-04-17-10_59_32" src="https://github.com/user-attachments/assets/5ac40773-331e-45a2-8552-a10869cc77f0" />

Efter det så klicka review + create och sen create igen. Klart med detta steg.

### Skapa en container i din Blob Storage
Next up är att skapa en container inuti din blob storage.

Gå tillbaka till startsidan och klicka in på din nyskapta blob storage.

Kolla efter "containers" i vänstra panelen och tryck in där:
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/df4767b4-e929-42de-80f0-835f5e3a7c73" />

Tyck på Add Container i container vyn och fyll i namnet, se till att komma ihåg det till senare då man behöver referera till det i koden:
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/fd0e4f88-1bbb-416c-b613-2faaec7f30e8" />

När det är klart är det enda kvar att se till att man hittar Access Keys menyn (återigen på vänstra panelen) när dess information väl behövs så hittar man det, speciellt ConnectionString:
OBS: Du kan ta från Key1 och Key2, spelar ingen roll.
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/3026053d-2460-4e78-a1c9-c6540cab25ae" />
