#!/bin/bash
dotnet DiShelved.dll --urls http://0.0.0.0:${PORT:-80}
