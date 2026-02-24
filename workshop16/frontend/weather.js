(function () {
	// Load and render weather data from backend
	document.addEventListener('DOMContentLoaded', () => {
		const tbody = document.getElementById('weather-data');
		if (!tbody) return;

		// Create/attach a status element above the table for messages
		let status = document.getElementById('status');
		if (!status) {
			status = document.createElement('div');
			status.id = 'status';
			const table = tbody.closest('table');
			table.parentNode.insertBefore(status, table);
		}

		const backendUrl = 'http://localhost:5033/weatherforecast/';
		status.textContent = 'Loading...';

		fetch(backendUrl)
			.then(res => {
				if (!res.ok) throw new Error('Server returned ' + res.status);
				return res.json();
			})
			.then(data => {
				status.textContent = '';
				if (!Array.isArray(data)) {
					status.textContent = 'Unexpected response format';
					return;
				}
				tbody.innerHTML = '';
				data.forEach(d => {
					const tr = document.createElement('tr');
					const date = new Date(d.date);
					const dateStr = isNaN(date) ? d.date : date.toLocaleDateString();
					tr.innerHTML = `
						<td>${dateStr}</td>
						<td style="text-align:right">${d.temperatureC}</td>
						<td style="text-align:right">${d.temperatureF}</td>
						<td>${d.summary ?? ''}</td>
					`;
					tbody.appendChild(tr);
				});
			})
			.catch(err => {
				status.textContent = 'Error: ' + err.message + (err.name === 'TypeError' ? ' (possible CORS or network issue)' : '');
			});
	});
})();

