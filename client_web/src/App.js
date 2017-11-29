import React, { Component } from 'react';
import logo from './lfglogo.svg.jpg';
import './App.css';
import axios from 'axios';
import Game from './game';

class App extends Component {
    constructor(props) {
      super(props);
  
      this.state = { 
        games: [],
    };
      this.get_games = this.get_games.bind(this);
      this.searchFunction = this.searchFunction.bind(this);
    }


  get_games(event) {
    axios.get(`http://localhost:61016/game`).then(
      (response) => {
        response.data.sort((a, b) => {
          if (a.name > b.name) 
          {
            return 1; 
          }

          if (a.name == b.name) 
          {
            return 0; 
          }

            return -1; 
        })
        this.setState({games: response.data})   
      }
    );
  }

  componentWillMount() {
    this.get_games()
  }

  searchFunction(){
    var searchResults = this.state.games.filter((variable) => {
      return variable.name.includes(this.state.searchText)
        
    })
    //console.log (this.state.searchText)
    this.setState({games: searchResults})

  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Welcome to LFG! Please select a game!</h1>
        </header>
        <h2>
        <input value={this.state.searchText}type="text" onChange={e => this.setState({searchText: e.target.value})} id="variable" placeholder="Search"></input>
        <button type="submit" onClick={this.searchFunction}>Click to Search</button>
        <span>                     </span>
          <button className="sortBy" >Filter by genre</button>
          
        
        </h2>
        {this.state.games.map((game, index) => {
          return <Game fullGame={game} key={index} />;
        } )
        }
      </div>
    );
  }
}

export default App;