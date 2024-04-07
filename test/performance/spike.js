import http from 'k6/http';

export let options = {
    stages: [
        // Normal load of 10 VUs for the first 10 seconds
        { duration: '2s', target: 10 },
        // Spike to 30 VUs over the next 10 seconds (ramp-up)
        { duration: '2s', target: 30 },
        // Hold spike at 30 VUs for 10 seconds
        { duration: '2s', target: 30 },
        // Ramp-down to 10 VUs over the next 10 seconds
        { duration: '2s', target: 10 },
        // Repeat the pattern to ensure the test runs for approximately 2 minutes
        // Second cycle begins
        { duration: '2s', target: 10 },
        { duration: '2s', target: 30 },
        { duration: '2s', target: 30 },
        { duration: '2s', target: 10 },
        // Third cycle begins
        { duration: '2s', target: 10 },
        { duration: '2s', target: 30 },
        { duration: '2s', target: 30 },
        { duration: '2s', target: 10 },
        // Ensure we end with a normal load
        { duration: '2s', target: 10 },
    ],
    thresholds: {
        // You can define thresholds for your test here
        http_req_duration: ['p(95)<500'], // 95% of requests should be below 500ms
    },
};

export default function () {
    http.get("http://167.86.105.61:5002/currencyconverter");
}