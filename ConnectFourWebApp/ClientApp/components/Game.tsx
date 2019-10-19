import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

export class Game extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <h1>Hello, world!</h1>
        </div>;
    }
}
