#!/usr/bin/env bash
set -euo pipefail

# Detect platform
OS=$(uname -s)
case "$OS" in
  Linux*)   PLATFORM="Linux" ;;
  Darwin*)  PLATFORM="macOS" ;;
  MINGW*|MSYS*|CYGWIN*) PLATFORM="Windows (Git Bash/WSL)" ;;
  *)        PLATFORM="Unknown" ;;
esac
echo "Detected platform: $PLATFORM"

# Backend
echo "Starting backend..."
pushd backend > /dev/null
dotnet ef database update     
dotnet run &
BACKEND_PID=$!
popd > /dev/null

# Frontend
echo "Starting frontend..."
pushd frontend > /dev/null
npm install                   
npm start &                   
FRONTEND_PID=$!
popd > /dev/null

echo "Press Ctrl+C to stop running backend and frontend."

wait $BACKEND_PID $FRONTEND_PID