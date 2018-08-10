import React, { Component } from 'react';

export class Project extends Component {
  displayName = Project.name

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.state = {
                    input: {
                        firstName: "",
                        lastName: ""
                    },
                    blurred: {
                      firstName: false,
                      lastName: false
                    }
                  };

  }

  incrementCounter() {
    fetch('api/Project', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        firstParam: 'yourValue',
        secondParam: 'yourOtherValue',
      })
    })
  }

  handleInputChange(newPartialInput) {
    this.setState(state => ({
        ...state,
        input: {
            ...state.input,
            ...newPartialInput
        }
    }))
}

  handleChange(event) {
    this.setState({value: event.target.value});
  }

  handleSubmit(event) {
    alert('A project was submitted: ' + this.state.input.firstName + ':'+this.state.input.lastName);
    event.preventDefault();
    fetch('api/Project', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        firstName: this.state.input.firstName,
        lastName: this.state.input.lastName,
      })
    })
  }

  render() {
    if (String(this.props.location.pathname) === "/project/create"){
      return this.renderForm();
    }else{

      var ProperListRender = React.createClass({displayName: "ProperListRender",
        render: function() {
          return (
            React.createElement("ul", null,
              this.props.list.map(function(listValue){
                return React.createElement("li", null, listValue);
              })
            )
          )
        }
      });
      React.render(React.createElement(ProperListRender, {list: [1,2,3,4,5]}), document.getElementById('proper-list-render1'));
      React.render(React.createElement(ProperListRender, {list: [1,2,3,4,5,6,7,8,9,10]}), document.getElementById('proper-list-render2'));
      
      return (
        <div>
          <h1>Counter</h1>
          <p>This is a simple example of a React component.</p>
          <p>Current count: <strong>{this.state.currentCount}</strong></p>
          <button onClick={this.incrementCounter}>Increment</button>
        </div>
      );
    }
  }

  componentDidMount(){
    this.firstName.focus();
  }
  handleBlur(fieldName) {
    this.setState(state => ({
        ...state,
        blurred: {
            ...state.blurred,
            [fieldName]: true
        }
    }))
  }
  validate() {
    const errors = {};
    const {input} = this.state;
    
    if (!input.firstName || input.firstName.length<10) {
        errors.firstName = 'firstName is required and must be at least 10 characters  ';
    } 

    if (!input.lastName) {
        errors.lastName = 'lastName is required';
    }

    return {
        errors,
        isValid: Object.keys(errors).length === 0
    };
}

  renderForm() {
    const {input, blurred} = this.state;
    const {errors, isValid} = this.validate();
    
    return (
      <form onSubmit={this.handleSubmit}>
        <div>
          <label>
          firstName:
            <input 
              name="firstName" 
              ref={(input) => { this.firstName = input; }} 
              onBlur={() => this.handleBlur('firstName')}
              type="text" value={input.firstName} 
              onChange={e => this.handleInputChange({firstName: e.target.value})} 
            />
          </label>
          {blurred.firstName && !!errors.firstName && <span>{errors.firstName}</span>}
        </div>
        <div>
          <label>
          lastName:
          <input 
              name="lastName" 
              onBlur={() => this.handleBlur('lastName')}
              type="text" value={input.lastName} 
              onChange={e => this.handleInputChange({lastName: e.target.value})} 
            />
          </label>
          {blurred.lastName && !!errors.lastName && <span>{errors.lastName}</span>}
        </div>
        <button type="submit" disabled={!isValid}>
                        Submit
                    </button>
      </form>
    );

    
  }

  

  
}