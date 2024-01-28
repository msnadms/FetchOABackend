    git clone https://github.com/msnadms/FetchOABackend
    cd FetchOABackend
    docker build -t "fetchoa:Dockerfile" .
    docker run -p 8000:80 fetchoa
    curl -X POST -H "Content-Type: text/json" --data-binary @example.json localhost:8000/receipts/process
    curl http://localhost:8000/receipts/<uuid>/points
