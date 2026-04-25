import { useState } from 'react';

function App() {
  const [url, setUrl] = useState('');
  const [shortUrl, setShortUrl] = useState('');
  const [loading, setLoading] = useState(false);
  const [copied, setCopied] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!url) return;
    setLoading(true);

    try {
      const response = await fetch('http://localhost:5056/api/Links/shorten', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json' // Вказуємо, що шлемо JSON
        },
        // Відправляємо саме рядок у форматі JSON (напр. "https://google.com")
        body: JSON.stringify(url)
      });

      if (response.ok) {
        const data = await response.json();
        setShortUrl(data.shortUrl);
      } else {
        const errorText = await response.text();
        alert(`Помилка сервера: ${errorText}`);
      }
    } catch (err) {
      alert("Сервер не відповідає. Перевір, чи запущено API в Rider.");
    } finally {
      setLoading(false);
    }
  };

  const copyToClipboard = () => {
    navigator.clipboard.writeText(shortUrl);
    setCopied(true);
    setTimeout(() => setCopied(false), 2000);
  };

  const styles = {
    main: {
      height: '100vh', display: 'flex', alignItems: 'center', justifyContent: 'center',
      background: 'linear-gradient(135deg, #0f172a 0%, #1e293b 100%)', fontFamily: '"Inter", sans-serif', color: '#fff'
    },
    card: {
      background: 'rgba(30, 41, 59, 0.7)', backdropFilter: 'blur(10px)', padding: '40px',
      borderRadius: '24px', border: '1px solid rgba(255,255,255,0.1)', width: '100%', maxWidth: '400px', textAlign: 'center'
    },
    input: {
      width: '100%', padding: '14px', borderRadius: '12px', border: '1px solid #334155',
      backgroundColor: '#0f172a', color: '#fff', marginBottom: '16px', outline: 'none', fontSize: '15px'
    },
    button: {
      width: '100%', padding: '14px', borderRadius: '12px', border: 'none',
      backgroundColor: '#3b82f6', color: '#fff', fontWeight: 'bold', cursor: 'pointer', transition: '0.3s'
    },
    result: {
      marginTop: '25px', padding: '15px', borderRadius: '12px', backgroundColor: 'rgba(16, 185, 129, 0.1)',
      border: '1px solid #10b981', display: 'flex', flexDirection: 'column', gap: '10px'
    }
  };

  return (
      <div style={styles.main}>
        <div style={styles.card}>
          <h1 style={{ margin: '0 0 10px 0', fontSize: '28px', color: '#60a5fa' }}>SmartLink</h1>
          <p style={{ color: '#94a3b8', marginBottom: '30px', fontSize: '14px' }}>Швидке скорочення посилань</p>

          <form onSubmit={handleSubmit}>
            <input
                type="text" placeholder="Вставте довгий URL..."
                value={url} onChange={(e) => setUrl(e.target.value)} style={styles.input}
            />
            <button type="submit" style={styles.button} disabled={loading}>
              {loading ? 'Зачекайте...' : 'Скоротити'}
            </button>
          </form>

          {shortUrl && (
              <div style={styles.result}>
                <a href={shortUrl} target="_blank" rel="noreferrer" style={{ color: '#10b981', textDecoration: 'none', fontWeight: '600', wordBreak: 'break-all' }}>
                  {shortUrl}
                </a>
                <button onClick={copyToClipboard} style={{ background: 'none', border: 'none', color: '#60a5fa', cursor: 'pointer', fontSize: '13px' }}>
                  {copied ? '✅ Скопійовано!' : '📋 Копіювати'}
                </button>
              </div>
          )}
        </div>
      </div>
  );
}

export default App;