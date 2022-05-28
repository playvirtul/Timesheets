import 'antd/dist/antd.css';
import React from 'react';
import logo from './logo.svg';
import './App.css';
import SiderDemo from './pages/SiderDemo';
import MainPage from './pages/MainPage';

function App() {
  return (
    <div className="App">
      <SiderDemo/>
      <MainPage/>
    </div>
  );
}

export default App;
