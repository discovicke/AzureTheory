const input = document.getElementById('input');

async function UploadAsync() {
    try {
        const file = input.files[0];
        console.log(file);

        if (!file) {
            return;
        }

        const formData = new FormData();
        formData.append("file", file);

        const response = await fetch('/upload', {
            method: 'POST',
            body: formData
        });

        if (!response.ok) {
            throw new Error('Fel');
        }

        const data = await response.json();
         
    } catch (error) {
        console.error("Fel!" + error);
    }
}

async function DownloadAsync() {
    //TODO : skriv!
    try {
        const response = await fetch('/download/{id}', {
            method: 'GET'
        });
        if (!response.ok) {
            throw new Error('Fel');
        }
        const blob = await response.blob();
        
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = 'filnamn.ext';
        a.click();
        URL.revokeObjectURL(url);
    } catch (error) {
        console.error("Fel!");
    }
}

async function PreviewImgage() {
    //TODO : skriv!
}
