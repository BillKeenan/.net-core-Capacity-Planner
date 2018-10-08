import React, { Component } from 'react';

export class Project extends Component {
  displayName = Project.name

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.addProject = this.addProject.bind(this);

    this.state = { projects: [], loading: true, }

    fetch('api/Project')
      .then(response => response.json())
      .then(data => {
        this.setState({ projects: data, loading: false, });
      });

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

  addProject() {

    var project = {
      name: 'Test Project'
    };

    (async () => {
      const rawResponse = await fetch('api/Project', {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(project)
      });
      const content = await rawResponse.json();
      this.setState(prevState => ({
        projects: [...prevState.projects, project]
      }))
    })();

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
    this.setState({ value: event.target.value });
  }


  handleSubmit(event) {

    this.setState(state => ({
      ...state,
      projects: {
        ...state.projects,
        ...{name:this.state.input.name}
      }
    }))

    event.preventDefault();
    fetch('api/Project', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        firstName: this.state.input.name
      })
    })
  }


  render() {
    // like this https://reactjs.org/docs/thinking-in-react.html
    if (this.state.loading) {
      return (<div>loading</div>);
    } else {
      var week = 0;
      var count = 9;


      return (
        <div>
          <div>
            <table className="projectCapacity">
              <tr>
                <td>
                </td>
                  <CapacityHeader week={week} count={count} />
              </tr>
              <ProjectListItem week={week} count={count} projects={this.state.projects} />
            </table>
          </div>
          <div>
            <h1>Counter</h1>
            <p>This is a simple example of a React component.</p>
            <p>Current count: <strong>{this.state.currentCount}</strong></p>
            <button onClick={this.addProject}>Increment</button>
          </div>
        </div>
      );
    }
  }

  componentDidMount() {
    if (String(this.props.location.pathname) === "/project/create") {

      this.Name.focus();
    }
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
    const { input } = this.state;

    if (!input.name || input.name.length < 10) {
      errors.name = 'Name is required and must be at least 10 characters  ';
    }
    return {
      errors,
      isValid: Object.keys(errors).length === 0
    };
  }

  renderForm() {
    const { input, blurred } = this.state;
    const { errors, isValid } = this.validate();

    return (
      <form onSubmit={this.handleSubmit}>
        <div>
          <label>
            Name:
            <input
              name="Name"
              ref={(input) => { this.name = input; }}
              onBlur={() => this.handleBlur('name')}
              type="text" value={input.name}
              onChange={e => this.handleInputChange({ name: e.target.value })}
            />
          </label>
          {blurred.name && !!errors.name && <span>{errors.name}</span>}
        </div>

        <button type="submit" disabled={!isValid}>
          Submit
                    </button>
      </form>
    );


  }

}

export class CapacityHeader extends Component {

  constructor(props) {
    super(props);
    this.state = { capacity: [], loading: true };

    var week = props.week;
    var count = props.count;

    fetch('api/CapacityHeader/' + week + '/' + count)
      .then(response => response.json())
      .then(data => {
        this.setState({ capacity: data, loading: false, });
      });
  }

  render() {

    if (this.state.loading) {
      return ("loading");
    }
    return (
      this.state.capacity.map(function (d, idx) {
        return <td>{new Intl.DateTimeFormat('en-GB', { 
          month: 'short', 
          day: 'numeric' 
        }).format(new Date(d))}</td>
      }, this)
    );
  }

}

export class ProjectListItem extends Component {

  constructor(props) {
    super(props);

    this.state = { projects: props.projects, week: props.week, count: props.count }

  }

  render() {
    return (
      this.state.projects.map(function (d, idx) {
        return <tr>
          <td key={idx}>{d.name}</td> {idx}<ProjectCapacity week={this.state.week} count={this.state.count} project={d.name} />
        </tr>
      }, this)
    );
  }
}

export class ProjectCapacity extends Component {

  constructor(props) {
    super(props);

    this.state = { capacity: [], loading: true };

    var week = props.week;
    var prj = props.project;
    var count = props.count;

    fetch('api/Capacity/' + prj + '/' + week + '/' + count)
      .then(response => response.json())
      .then(data => {
        this.setState({ capacity: data, loading: false, });
      });
  }

  render() {

    if (this.state.loading) {
      return ("loading");
    }
    return (
      this.state.capacity.map(function (d, idx) {
        return <td>{d}</td>
      }, this)
    );
  }
}

/* For a given date, get the ISO week number
*
* Based on information at:
*
*    http://www.merlyn.demon.co.uk/weekcalc.htm#WNR
*
* Algorithm is to find nearest thursday, it's year
* is the year of the week number. Then get weeks
* between that date and the first day of that year.
*
* Note that dates in one year can be weeks of previous
* or next year, overlap is up to 3 days.
*
* e.g. 2014/12/29 is Monday in week  1 of 2015
*      2012/1/1   is Sunday in week 52 of 2011
*/
function getWeekNumber(d) {
  // Copy date so don't modify original
  d = new Date(Date.UTC(d.getFullYear(), d.getMonth(), d.getDate()));
  // Set to nearest Thursday: current date + 4 - current day number
  // Make Sunday's day number 7
  d.setUTCDate(d.getUTCDate() + 4 - (d.getUTCDay() || 7));
  // Get first day of year
  var yearStart = new Date(Date.UTC(d.getUTCFullYear(), 0, 1));
  // Calculate full weeks to nearest Thursday
  var weekNo = Math.ceil((((d - yearStart) / 86400000) + 1) / 7);
  // Return array of year and week number
  return [d.getUTCFullYear(), weekNo];
}

var result = getWeekNumber(new Date());
document.write('It\'s currently week ' + result[1] + ' of ' + result[0]);