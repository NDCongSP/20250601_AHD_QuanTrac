let chart;

function formatVietnameseNumber(value) {
    const str = value.toString();
    const parts = str.split('.');
    
    // Add commas to integer part
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    
    // Join with dot for decimal part
    return parts.join('.');
}

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
                    label: 'Flow_Ho_Final',
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
                    type: 'linear',
                    bounds: 'data',
                    min: minX,
                    max: maxX,
                    title: { display: true, text: 'Vị trí' },
                    grid: { 
                        display: true,
                        color: '#eee'
                    },
                    ticks: {
                        stepSize: 2.4,   // ✅ tạo bước X đều 2.4 (giống Y)
                        callback: function(value) {
                            const found = data.find(d => Math.abs(d.x_Value - value) < 0.01);
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
                    beginAtZero: false,
                    min: -2,
                    afterBuildTicks: axis => {
                        // ✅ tạo tick Y bắt đầu từ -2, sau đó +2.4 cho đến max
                        const ticks = [];
                        let value = -2;
                        const max = axis.max || 20;
                        while (value <= max) {
                            ticks.push({ value: Math.round(value * 10) / 10 });
                            value = Math.round((value + 2.4) * 10) / 10;
                        }
                        axis.ticks = ticks;
                    },
                    ticks: {
                        callback: value => value
                    }
                }
            },
            plugins: {
                legend: {
                    position: 'top'
                },
                tooltip: {
                    callbacks: {
                        label: context => `${context.dataset.label}: ${context.parsed.y.toFixed(2)}m`
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

    // Ensure plugins are registered
    if (window.ChartDataLabels && !Chart.registry.plugins.get('datalabels')) {
        Chart.register(window.ChartDataLabels);
    }
    if (window['chartjs-plugin-annotation'] && !Chart.registry.plugins.get('annotation')) {
        // Annotation plugin auto-registers when imported via CDN in Chart.js v4
    }
    // Register region label plugin once
    if (!window._regionLabelPluginRegistered) {
        const regionLabelPlugin = {
            id: 'regionLabel',
            afterDatasetsDraw(chart, args, pluginOptions) {
                if (pluginOptions && pluginOptions.enabled === false) {
                    return; // allow disabling via options.plugins.regionLabel.enabled=false
                }
                const { ctx } = chart;
                const opts = pluginOptions || {};
                const get = (obj, path, fallback) => {
                    try {
                        return path.split('.').reduce((o, k) => (o && o[k] != null ? o[k] : undefined), obj) ?? fallback;
                    } catch { return fallback; }
                };

                // Use Chart.js internal order so top datasets draw last (higher z-order label wins visually)
                const metas = chart.getSortedVisibleDatasetMetas();
                ctx.save();
                ctx.textAlign = 'center';
                ctx.textBaseline = 'middle';

                metas.forEach((meta) => {
                    const ds = chart.data.datasets[meta.index];
                    if (!ds || !meta || !meta.data || meta.hidden || ds.hidden) return;
                    const isFilled = get(ds, 'fill', false) || get(ds, 'options.fill', false);
                    if (!isFilled) return; // only for filled areas

                    const dsOpts = ds.regionLabel || {};
                    const color = dsOpts.color ?? opts.color ?? '#2c3e50';
                    const opacity = dsOpts.opacity ?? opts.opacity ?? 0.85;
                    const padY = dsOpts.paddingY ?? opts.paddingY ?? 0;
                    const shadow = dsOpts.shadow ?? opts.shadow ?? false;
                    const font = {
                        size: (dsOpts.font && dsOpts.font.size) ?? (opts.font && opts.font.size) ?? 12,
                        weight: (dsOpts.font && dsOpts.font.weight) ?? (opts.font && opts.font.weight) ?? '600',
                        family: (dsOpts.font && dsOpts.font.family) ?? (opts.font && opts.font.family) ?? 'sans-serif'
                    };
                    const text = (dsOpts.text != null ? dsOpts.text : (ds.label || '')) + '';

                    const points = meta.data;
                    if (!points || points.length < 2) return;

                    const xs = [];
                    const ysRaw = [];
                    for (let i = 0; i < points.length; i++) {
                        const p = points[i];
                        if (!p || !isFinite(p.x) || !isFinite(p.y)) continue;
                        xs.push(p.x);

                        const raw = ds.data && ds.data[i];
                        const yVal = raw != null && typeof raw === 'object' && raw.y != null ? Number(raw.y)
                                     : typeof raw === 'number' ? Number(raw) : NaN;
                        if (isFinite(yVal)) ysRaw.push(yVal);
                    }
                    if (xs.length < 2 || ysRaw.length === 0) return;

                    const xMid = (Math.min(...xs) + Math.max(...xs)) / 2;
                    const yAvgRaw = ysRaw.reduce((a, b) => a + b, 0) / ysRaw.length;
                    const yMid = meta.yScale.getPixelForValue(yAvgRaw);

                    const area = chart.chartArea;
                    const yClamped = Math.max(area.top + font.size, Math.min(area.bottom - font.size, yMid + padY));

                    ctx.save();
                    ctx.globalAlpha = opacity;
                    if (shadow) { ctx.shadowColor = 'rgba(255,255,255,0.9)'; ctx.shadowBlur = 3; }
                    ctx.font = `${font.weight} ${font.size}px ${font.family}`;
                    ctx.fillStyle = color;
                    ctx.fillText(text, xMid, yClamped);
                    ctx.restore();
                });

                ctx.restore();
            }
        };
        Chart.register(regionLabelPlugin);
        window._regionLabelPluginRegistered = true;
    }

    // Convert function-like strings in options to real functions (for callbacks)
    const reviveFunctions = (obj) => {
        if (!obj || typeof obj !== 'object') return obj;
        for (const k of Object.keys(obj)) {
            const v = obj[k];
            if (typeof v === 'string' && v.trim().startsWith('function')) {
                try { obj[k] = eval('(' + v + ')'); } catch { /* ignore */ }
            } else if (v && typeof v === 'object') {
                reviveFunctions(v);
            }
        }
        return obj;
    };
    reviveFunctions(config);

    // Apply common visual tweaks: smaller points, larger hit area, span gaps
    if (config && config.data && Array.isArray(config.data.datasets)) {
        config.data.datasets = config.data.datasets.map(ds => {
            const clone = { ...ds };
            // Reduce dot size uniformly for visual clarity
            clone.pointRadius = 2;
            clone.pointHoverRadius = 4;
            clone.pointHitRadius = (typeof ds.pointHitRadius === 'number') ? ds.pointHitRadius : 8;
            clone.spanGaps = true; // allow gaps if there are nulls

            // Convert y<=0 to null to prevent dropping to X-axis and only connect points with value > 0
            if (Array.isArray(ds.data)) {
                clone.data = ds.data.map(p => {
                    if (p == null) return null;
                    if (typeof p === 'object') {
                        const y = p.y;
                        if (y == null || y <= 0) return { ...p, y: null };
                        return p;
                    }
                    if (typeof p === 'number' && p <= 0) return null;
                    return p;
                });
            }

            return clone;
        });
    }

    // Custom value labels only for target datasets (e.g., VungB, VungC)
    const meta = config.meta || {};
    const labelTargets = Array.isArray(meta.valueLabelTargets) ? meta.valueLabelTargets : [];
    // Register lightweight custom plugin for value labels (no external deps)
    if (!window._valueLabelPluginRegistered) {
        const valueLabelPlugin = {
            id: 'valueLabelPlugin',
            afterDatasetsDraw(chart, args, pluginOptions) {
                const { ctx, chartArea, scales } = chart;
                const opts = pluginOptions || {};
                const targets = opts.targets || [];
                const color = opts.color || '#333';
                const fontSize = opts.fontSize || 10;
                const fontWeight = opts.fontWeight || '600';
                const offset = opts.offset || 4;
                const minDistance = (typeof opts.minDistance === 'number') ? opts.minDistance : 18; // px between labels
                const every = (typeof opts.every === 'number' && opts.every > 0) ? Math.floor(opts.every) : 1; // draw every N-th
                ctx.save();
                ctx.font = `${fontWeight} ${fontSize}px sans-serif`;
                ctx.fillStyle = color;
                chart.data.datasets.forEach((dataset, datasetIndex) => {
                    const label = dataset.label || '';
                    const matchesTarget = Array.isArray(targets) && targets.some(t => label === t || (label && label.startsWith(t)));
                    if (!matchesTarget) return;
                    const meta = chart.getDatasetMeta(datasetIndex);
                    // Respect dataset visibility (legend toggle)
                    if (!meta || !meta.data || meta.hidden || dataset.hidden) return;
                    let lastX = -Infinity;
                    dataset.data.forEach((dp, i) => {
                        if (every > 1 && (i % every) !== 0) return;
                        const node = meta.data[i];
                        if (!node) return;
                        const raw = (typeof dp === 'object') ? (dp && dp.y) : dp;
                        if (raw == null) return;
                        const val = Number(raw);
                        if (!isFinite(val)) return;
                        const { x, y } = node.tooltipPosition(true);
                        if (Math.abs(x - lastX) < minDistance) return;
                        ctx.textAlign = 'center';
                        ctx.textBaseline = 'bottom';
                        ctx.fillText(val.toFixed(2), x, y - offset);
                        lastX = x;
                    });
                });
                ctx.restore();
            }
        };
        Chart.register(valueLabelPlugin);
        window._valueLabelPluginRegistered = true;
    }
    // Provide plugin options
    config.options = config.options || {};
    config.options.plugins = config.options.plugins || {};
    config.options.plugins.valueLabelPlugin = {
        targets: labelTargets,
        color: '#333',
        fontSize: 10,
        fontWeight: '600',
        offset: 4,
        minDistance: 22,
        every: 2
    };

    // Global options for region labels (can be overridden by dataset.regionLabel)
    config.options.plugins.regionLabel = config.options.plugins.regionLabel || {
        color: '#2c3e50',
        opacity: 0.85,
        paddingY: 0,
        shadow: false,
        font: { size: 12, weight: '600', family: 'sans-serif' }
    };
    // Disable area labels per request
    config.options.plugins.regionLabel.enabled = false;

    // Ensure tooltip works well for both line points and filled areas
    config.options.plugins.tooltip = config.options.plugins.tooltip || {};
    const tt = config.options.plugins.tooltip;
    tt.intersect = false; // easier to hover
    tt.mode = 'nearest';
    tt.filter = function(item) { return item.parsed && item.parsed.y != null; };
    tt.callbacks = tt.callbacks || {};
    if (!tt.callbacks.title) {
        tt.callbacks.title = function(items) {
            if (!items || !items.length) return '';
            const ms = items[0].parsed.x || (items[0].raw && items[0].raw.x);
            if (!ms) return '';
            try { return luxon.DateTime.fromMillis(ms).toFormat('dd/MM/yyyy HH:mm'); } catch { return ''; }
        };
    }
    tt.callbacks.label = function(context) {
        const y = context.parsed && context.parsed.y != null ? context.parsed.y : (context.raw && context.raw.y);
        const lab = context.dataset && context.dataset.label ? context.dataset.label : '';
        return `${lab}: ${Number(y).toFixed(2)}m`;
    };

    // Build region labels using annotation boxes if provided via config.meta.regions
    if (meta.regions && Array.isArray(meta.regions)) {
        config.options.plugins.annotation = config.options.plugins.annotation || { annotations: {} };
        const anns = config.options.plugins.annotation.annotations || {};
        meta.regions.forEach((r, idx) => {
            anns['region_' + idx] = {
                type: 'box',
                xMin: r.xMin,
                xMax: r.xMax,
                yMin: (typeof r.yMin === 'number') ? r.yMin : undefined,
                yMax: (typeof r.yMax === 'number') ? r.yMax : undefined,
                backgroundColor: r.backgroundColor || 'rgba(180,180,180,0.12)',
                borderWidth: 0,
                label: {
                    display: (r.showLabel !== false),
                    content: r.label || '',
                    position: 'center',
                    color: r.color || '#333',
                    backgroundColor: r.labelBg || 'rgba(255,255,255,0.9)',
                    padding: 4,
                    font: { style: 'bold' }
                }
            };
        });
        config.options.plugins.annotation.annotations = anns;
    }

    if (chart) {
        chart.destroy();
    }
    chart = new Chart(ctx, config);
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

function updateZThucValue(data) {
    if (!window.riverChart) {
        console.warn('Chart not initialized');
        return;
    }
    
    // Kiểm tra data và datasets tồn tại
    if (!window.riverChart.data || !window.riverChart.data.datasets) {
        console.warn('Chart data or datasets not available');
        return;
    }
    
    // Tìm dataset Z_Thực và update dữ liệu
    const zThucDataset = window.riverChart.data.datasets.find(ds => ds.label === 'Mực nước trên sông Sài Gòn');
    if (zThucDataset) {
        // Update dữ liệu cho Z_Thực
        zThucDataset.data = data.map(d => ({
            x: d.x_Value,
            y: d.z_ThucValue,
            prefix: d.x_Prefix
        }));
        
        // Update chart
        window.riverChart.update('none'); // 'none' để không có animation
    } else {
        console.warn('Z_Thực dataset not found');
    }
}

function updateChartWaterLevel(data) {
    const ctx = document.getElementById('riverChart');
    if (!ctx) {
        console.error('Canvas element riverChart not found');
        return;
    }
    const datasets = [
        { label: "Bờ phải", field: "boPhai", borderColor: "#C44B3E", borderDash: [] },
        { label: "Bờ trái", field: "boTrai", borderColor: "#3465A4", borderDash: [] },
        // { label: "Q300", field: "q300", borderColor: "#00796B", borderDash: [5, 5] },
        // { label: "Q400", field: "q400", borderColor: "#8E44AD", borderDash: [5, 5] },
        // { label: "Q600", field: "q600", borderColor: "#F39C12", borderDash: [5, 5] },
        // { label: "Q2800", field: "q2800", borderColor: "#B71C1C", borderDash: [5, 5] },
        { label: "Mực nước trên sông Sài Gòn", field: "z_ThucValue", borderColor: "#0078D7", borderDash: [] },
      ].map(s => ({
        label: s.label,
        data: data.map(d => ({ x: d.x_Value, y: d[s.field], prefix: d.x_Prefix })),
        borderColor: s.borderColor,
        backgroundColor: s.borderColor + "33",
        fill: false,
        tension: 0.3,
        borderWidth: 2,
        borderDash: s.borderDash,
        pointRadius: 2,
        pointHoverRadius: 4
      }));
// Keep original values (including <= 0) so the chart follows the true series
datasets.forEach(ds => { ds.spanGaps = true; });
    // Compute precise x-range to avoid extra right padding
    const minX = Math.min.apply(null, data.map(d => d.x_Value));
    const maxX = Math.max.apply(null, data.map(d => d.x_Value));
    // Compute global min Y across all datasets (ignoring nulls) to use as baseline
    const allY = [];
    datasets.forEach(ds => {
        if (Array.isArray(ds.data)) {
            ds.data.forEach(p => { if (p && p.y != null && isFinite(p.y)) allY.push(Number(p.y)); });
        }
    });
    const minY = allY.length ? Math.min.apply(null, allY) : undefined;
    const yStep = 2.4; // fixed tick step
    const minYRounded = (minY !== undefined) ? Math.floor(minY / yStep) * yStep : undefined;

    window.riverChart = new Chart(ctx.getContext('2d'), {
    type: 'line',
    data: { 
      labels: data.map(d => d.x_Prefix), // Use X_Prefix as labels
      datasets: datasets.map(dataset => {
        const isZThuc = dataset.label === 'Mực nước trên sông Sài Gòn';
        return {
          ...dataset,
          type: isZThuc ? 'line' : 'line',
          // Fill down to the minimum of Y scale instead of origin (0)
          fill: isZThuc ? 'start' : false,
          backgroundColor: isZThuc ? '#0078D733' : dataset.borderColor + "33",
          data: data.map((d, i) => ({
            x: d.x_Value,  // Use actual x_Value for positioning
            y: d[dataset.label === 'Bờ phải' ? 'boPhai' : 
                 dataset.label === 'Bờ trái' ? 'boTrai' : 
                 dataset.label === 'Q300' ? 'q300' : 
                 dataset.label === 'Q400' ? 'q400' : 
                 dataset.label === 'Q600' ? 'q600' :
                 dataset.label === 'Mực nước trên sông Sài Gòn' ? 'z_ThucValue' : 'q2800'],
              prefix: d.x_Prefix
          }))
        };
      })
    },
    options: {
      responsive: true,
      maintainAspectRatio: false, // allow canvas to use full container width/height
      layout: { padding: { left: 0, right: 0 } },
      interaction: { mode: 'index', intersect: false },
      plugins: {
        title: {
          display: false,
        },
        tooltip: {
          callbacks: {
            title: function(context) {
              const raw = context[0].raw;
              return raw && raw.prefix ? raw.prefix : '';
            },
            label: function(context) {
              const value = context.parsed.y;
              const formattedValue = formatVietnameseNumber(value.toFixed(2));
              return context.dataset.label + ": " + formattedValue + "m";
            }
          }
        },
        legend: { position: 'top' }
      },
      scales: {
        x: {
            type: 'linear',
            bounds: 'data', // fit exactly to data range
            min: minX,
            max: maxX,
            title: { display: true, text: 'Vị trí' },
            grid: { display: true, color: '#eee' },
            offset: false,
            afterBuildTicks: axis => {
              // Gán tick positions = danh sách x_Value thật
              axis.ticks = data.map(d => ({ value: d.x_Value })).filter(t => t.value !== 0);
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
            beginAtZero: false,
            min: -2,
            afterBuildTicks: axis => {
              // Ensure ticks include 0.4 by stepping 2.4 from -2 up to axis.max
              const ticks = [];
              let value = -2;
              const max = axis.max || 20;
              while (value <= max) {
                ticks.push({ value: Math.round(value * 10) / 10 });
                value = Math.round((value + 2.4) * 10) / 10;
              }
              axis.ticks = ticks;
            },
            ticks: {
              stepSize: 2.4,
              callback: function(value) {
                return value === 0 ? '' : value;
              }
            }
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
window.updateZThucValue = updateZThucValue;

window.setDotNetReference = function (dotNetReference) {
    window.dotNetReference = dotNetReference;
};