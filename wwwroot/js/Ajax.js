const Ajax = (props) => {

    let { url, cbSuccess } = props;

    const API_KEY = "Z2NFeGlpTXY2WDJwM05hdkhYQkxHRXdjdDdTVktXeDVSeW1LMmdQOA==";

    let headers = new Headers();
    headers.append("X-CSCAPI-KEY", API_KEY);

    let requestOptions = {
        method: 'GET',
        headers: headers,
        redirect: 'follow'
    };

    fetch(url, requestOptions)
        .then(res => res.ok ? res.json() : Promise.reject(res))
        .then(json => cbSuccess(json))
        .catch(err => {
            console.log(`Error: ${err}`)
        });

}

