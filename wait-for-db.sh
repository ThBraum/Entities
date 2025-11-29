#!/bin/bash
set -euo pipefail

host="${DB_HOST:-db}"
port="${DB_PORT:-5432}"
timeout_secs="${WAIT_TIMEOUT:-60}"

echo "Waiting for database at $host:$port (timeout ${timeout_secs}s)"

elapsed=0
while ! (echo > /dev/tcp/${host}/${port}) 2>/dev/null; do
    sleep 1
    elapsed=$((elapsed+1))
    if [ "$elapsed" -ge "$timeout_secs" ]; then
        echo "Timed out after ${timeout_secs}s waiting for ${host}:${port}" >&2
        exit 1
    fi
done

echo "Database ${host}:${port} is reachable. Starting application..."
exec dotnet Entidades.dll
