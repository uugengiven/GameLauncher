import React, { Component } from 'react';
import axios from 'axios';

class GameUsed extends Component {
    constructor(props){
        super(props);
        this.startGame = this.startGame.bind(this);
    }
    
    startGame(steamId){
        console.log("Ciao! Buongiorno!")
        axios.get(`http://localhost:8099/api/games/startgame/${this.props.fullGame.steamId}`)
         .then(function(response) {
           console.log(response);
          //here is where we will map out our responses
         
         }).catch(err => console.log(`Error: ${err}`));
 }
//if in one state, return this (all options in the render function):    
    render (){
        return(        
        <div className="gameUsed"> 
             <span className="gameTitle"> {this.props.fullGame.name}</span> 
             <img className="gameImage" src = {`http://cdn.akamai.steamstatic.com/steam/apps/${this.props.fullGame.steamId}/header.jpg?t=1510847069`}  
             /> 
             <div><span></span></div> 
        </div> 
        )
    }
}

export default GameUsed;
//Export default Game means this is a component and it will export itself, this makes it a public class