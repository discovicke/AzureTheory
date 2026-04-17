const input = document.getElementsById("input");

async function UploadAsync() {
    try {

        const file = input.file[0];

        if (!file) {
            return;
        }

        const formData = new FormData();
        formData.append("file", file);

        const response = await fetch('/upload/{id}', {
            method: 'POST',
            body: formData
        });

        if (!response.ok) {
            throw new Error('Fel');
        }

        const data = await response.json();
         
    } catch (error) {
        console.error("Fel!");
    }
}

async function GetImage() {

}