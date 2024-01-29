This uses C# ASP.NET with .NET 6.0.

Running with docker:
    
    git clone https://github.com/msnadms/FetchOABackend
    cd FetchOABackend
    docker build -t "fetchoa:Dockerfile" .
    docker run -p 8000:80 fetchoa
    curl -X POST -H "Content-Type: text/json" --data-binary @example.json localhost:8000/receipts/process
    curl http://localhost:8000/receipts/<uuid>/points

If .NET 6 is installed, this can be ran and tested using swagger - although if using docker swagger will not work, and should be tested normally.

Some notes: usually something like this would be done asynchronously but because this uses in-memory storage, there is no need for asynchronous operations.
