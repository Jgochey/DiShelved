#!/bin/bash
set -e

echo "Installing .NET 8 runtime..."

# Download and install .NET 8
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0 --runtime aspnetcore --install-dir /opt/dotnet

# Add dotnet to PATH
export PATH="/opt/dotnet:$PATH"

echo "Building .NET application..."
cd DiShelved
dotnet publish -c Release -o ../out

echo "Build completed successfully!"
