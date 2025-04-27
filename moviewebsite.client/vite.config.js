import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';

const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ''
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

const certificateName = "moviewebsite.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}

// Use the VITE_API_BASE_URL from the .env file
const target = env.VITE_API_BASE_URL || 'https://localhost:7040';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '^/api/category': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/episode': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/film': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/filmCate': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/images': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/video': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/authentication': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/payment': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/comment': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/userFilm': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/dashBoard': {
                target,
                secure: false,
                changeOrigin: true,
            },
            '^/api/account': {
                target,
                secure: false,
                changeOrigin: true,
            }
        },
        port: 5173,
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
});
