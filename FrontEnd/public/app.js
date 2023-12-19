function makeRequests() {
    const urls = ['https://localhost:7012/api/values', 'https://localhost:7012/api/values', 'https://localhost:7012/api/values'];

    Promise.all(urls.map(url =>
        fetch(url).then(response => response.text())
    )).then(texts => {
        console.log(texts);
    });
}
