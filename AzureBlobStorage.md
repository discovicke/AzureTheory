Azure Blob Storage är en Microsoft tjänst för att lagra objekt i molnet. Det är optimerat för att lagra stora mängder av ostrukterad data, och passar bra för att lagra exempelvis bilder, dokument, videor och loggfiler. Blob Storage hanterar alla typer av binär data (en BLOB = Binary Large Object).

Blob Storage kan leverera bilder eller dokument direkt till webbläsaren och passar exempelvis bra när man vill streama video/ljud, lagra säkerhetskopior, analysdata och data för molnappar. En användare eller klientprogram kan komma åt objekt i Blob Storage via HTTP/HTTPS-anrop. Objekten i Blob Storage är tillgängliga via Azure Storage REST API, Azure PowerShell, Azure CLI eller ett Azure Storage klientbibliotek, vilka finns tillgängliga för flera olika programmeringsspråk, bland annat för .NET.

Fördelar med att använda Blob Storage är bland annat en hög tillgänglighet för användaren, låga kostnader eftersom lagring sker i olika nivåer, samt att det finns inbyggda säkerhetsfunktioner.

Om vi tänker att  din kod i Program.cs är kocken som bestämmer vad som ska serveras, så är Azure Blob Storage den gigantiska lagerlokalen där du slänger in allt som är för stort för att få plats på skärbrädan.

För att förstå mer hur strukturen ser så kan vi tänka på, Om Blob Storage är det stora externa lagret, behöver vi ett system för att hitta rätt hylla och rätt låda. 
Innan du går in i lagret måste du förstå hierarkin. Det är inte bara en stor hög med saker, utan det är uppdelat i tre nivåer

- *Storage account* (Restaurangkedjan) Detta är själva kontot i Azure. Det är     paraplyet för allt ditt lagringsutrymme.

- *Container* (Rummet/Kylen) 
Inuti ditt konto skapar du "Containers". Tänk på det som olika rum i lagret – ett för "Bilder", ett för "Loggar", och ett för "Menyer". Du kan ha olika säkerhetsregler för olika rum (t.ex. att "Meny-rummet" är öppet för alla, men "Kvitto-rummet" är låst).

- *Blob* (Själva råvaran): Detta är den enskilda filen, t.ex. kalle_anka_profilbild.jpg.

![new blob.png](new%20blob.png)
