let chart;

function initializeChart(currentLevel) {
    const ctx = document.getElementById('reservoirChart');
    if (!ctx) return;

    const config = {
        type: 'line',
        data: {
            datasets: [
                {
                    label: 'Mực nước quan trắc',
                    data: [],
                    borderColor: 'magenta',
                    borderWidth: 2,
                    pointRadius: 3,
                    fill: false,
                    tension: 0.3
                },
                {
                    label: 'Flow_TL_CDD',
                    data: [],
                    showLine: false,
                    pointBackgroundColor: 'blue',
                    pointBorderColor: 'blue',
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    order: 10
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            interaction: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                x: {
                    type: 'time',
                    time: {
                        unit: 'day',
                        displayFormats: {
                            day: 'dd/MM'
                        },
                        tooltipFormat: 'dd/MM/yyyy HH:mm'
                    },
                    grid: {
                        display: true
                    },
                    ticks: {
                        maxRotation: 45,
                        minRotation: 45
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Mực nước (m)'
                    },
                    beginAtZero: false
                }
            },
            plugins: {
                legend: {
                    position: 'top'
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            return `${context.dataset.label}: ${context.parsed.y.toFixed(2)}m`;
                        }
                    }
                },
                annotation: {
                    annotations: {
                        currentLine: {
                            type: 'line',
                            yMin: currentLevel || 0,
                            yMax: currentLevel || 0,
                            borderColor: 'red',
                            borderWidth: 2,
                            borderDash: [6, 6],
                            label: {
                                enabled: true,
                                content: 'Mực nước hiện tại',
                                position: 'end',
                                backgroundColor: 'rgba(0, 0, 0, 0.8)',
                                color: 'white',
                                padding: 5,
                                borderRadius: 3
                            }
                        }
                    }
                }
            }
        }
    };

    chart = new Chart(ctx, config);
    window.reservoirChart = chart;

    // Add mousemove event for hover
    ctx.onmousove = handleChartHover;
}

function updateChart(config) {
    const ctx = document.getElementById('reservoirChart').getContext('2d');
    const infoLine2 = document.getElementById('infoLine2');
    const chart = new Chart(ctx, config);
    chart.canvas.addEventListener('mousemove', (event) => {
        const rect = chart.canvas.getBoundingClientRect();
        const x = event.clientX - rect.left;
        const y = event.clientY - rect.top;

        const yScale = chart.scales.y;
        const xScale = chart.scales.x;

        if (!xScale || !yScale) return;

        const yValue = yScale.getValueForPixel(y);
        const xValue = xScale.getValueForPixel(x);

        // Hiển thị trên dòng infoLine2
        if (yValue && xValue) {
            const date = luxon.DateTime.fromMillis(xValue).toFormat("dd/MM/yyyy HH:mm");
            //call c# function OnChartHover
            window.dotNetReference.invokeMethodAsync('OnChartHover', date, yValue);
        }
    });
}

function handleChartHover(event) {
    if (!chart) return;

    const points = chart.getElementsAtEventForMode(event, 'nearest', { intersect: true }, true);
    if (points.length) {
        const point = points[0];
        const dataset = chart.data.datasets[point.datasetIndex];
        const value = dataset.data[point.index];

        // Update info line
        if (window.dotNetReference) {
            window.dotNetReference.invokeMethodAsync('OnChartHover', value.y, value.x);
        }
    }
}

function updateInfoLine2(yValue, xValue) {
    const infoLine2 = document.getElementById('infoLine2');
    if (infoLine2) {
        const date = new Date(xValue).toLocaleString('vi-VN');
        infoLine2.textContent = `X: ${date} – Z: ${yValue.toFixed(2)}m`;
    }
}

function updateChartWaterLevel(data) {
    const ctx = document.getElementById('riverChart').getContext('2d');
    const datasets = [
        { label: "Bờ phải", field: "boPhai", borderColor: "#C44B3E", borderDash: [] },
        { label: "Bờ trái", field: "boTrai", borderColor: "#3465A4", borderDash: [] },
        { label: "Q300", field: "q300", borderColor: "#00796B", borderDash: [5, 5] },
        { label: "Q400", field: "q400", borderColor: "#8E44AD", borderDash: [5, 5] },
        { label: "Q600", field: "q600", borderColor: "#F39C12", borderDash: [5, 5] },
        { label: "Q2800", field: "q2800", borderColor: "#B71C1C", borderDash: [5, 5] },
        { label: "Z_Thực", field: "z_Thuc", borderColor: "#0078D7", borderDash: [] },
      ].map(s => ({
        label: s.label,
        data: data.map(d => ({ x: d.x_Value, y: d[s.field], prefix: d.x_Prefix })),
        borderColor: s.borderColor,
        backgroundColor: s.borderColor + "33",
        fill: false,
        tension: 0.3,
        borderWidth: 2,
        borderDash: s.borderDash,
        pointRadius: 3,
        pointHoverRadius: 5
      }));
    new Chart(ctx, {
    type: 'line',
    data: { 
      labels: data.map(d => d.x_Prefix), // Use X_Prefix as labels
      datasets: datasets.map(dataset => {
        const isZThuc = dataset.label === 'Z_Thực';
        return {
          ...dataset,
          type: isZThuc ? 'line' : 'line',
          fill: isZThuc ? 'origin' : false,
          backgroundColor: isZThuc ? '#0078D733' : dataset.borderColor + "33",
          data: data.map((d, i) => ({
            x: d.x_Value,  // Use actual x_Value for positioning
            y: d[dataset.label === 'Bờ phải' ? 'boPhai' : 
                 dataset.label === 'Bờ trái' ? 'boTrai' : 
                 dataset.label === 'Q300' ? 'q300' : 
                 dataset.label === 'Q400' ? 'q400' : 
                 dataset.label === 'Q600' ? 'q600' :
                 dataset.label === 'Z_Thực' ? 'z_Thuc' : 'q2800'],
              prefix: d.x_Value
          }))
        };
      })
    },
    options: {
      responsive: true,
      interaction: { mode: 'index', intersect: false },
      plugins: {
        title: {
          display: false,
        },
        tooltip: {
          callbacks: {
            title: function(context) {
              return context[0].raw.prefix;
            },
            label: function(context) {
              return context.dataset.label + ": " + context.parsed.y + "";
            }
          }
        },
        legend: { position: 'top' }
      },
      scales: {
        x: {
            type: 'linear',
            title: { display: true, text: 'Vị trí' },
            grid: { display: false },
            offset: false,
            afterBuildTicks: axis => {
              // Gán tick positions = danh sách x_Value thật
              axis.ticks = data.map(d => ({ value: d.x_Value }));
            },
            ticks: {
              callback: function(value) {
                // Tìm x_Prefix tương ứng với x_Value
                const found = data.find(d => d.x_Value === value);
                return found ? found.x_Prefix : '';
              },
              autoSkip: false,
              maxRotation: 90,
              minRotation: 90
            }
          },
        y: {
          title: { 
            display: true, 
            text: 'Cao trình (m)' 
          },
          grid: { 
            color: '#eee' 
          },
          beginAtZero: true
        }
      },
      elements: {
        line: { 
          tension: 0.3 
        }
      }
    }
  });
}
// Make functions available globally
window.initializeChart = initializeChart;
window.updateChart = updateChart;
window.updateInfoLine2 = updateInfoLine2;
window.dotNetReference = null;
window.updateChartWaterLevel = updateChartWaterLevel;

window.setDotNetReference = function (dotNetReference) {
    window.dotNetReference = dotNetReference;
};