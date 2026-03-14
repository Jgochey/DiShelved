#!/bin/bash
export PATH="/opt/dotnet:$PATH"
cd /opt/render/project/src
dotnet out/DiShelved.dll --urls http://0.0.0.0:$PORT
